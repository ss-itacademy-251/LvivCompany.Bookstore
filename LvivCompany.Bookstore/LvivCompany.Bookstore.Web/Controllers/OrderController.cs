using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LvivCompany.Bookstore.BusinessLogic.Mapper;
using LvivCompany.Bookstore.BusinessLogic.ViewModels;
using LvivCompany.Bookstore.DataAccess.Repo;
using LvivCompany.Bookstore.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LvivCompany.Bookstore.Web.Controllers
{
    [Authorize(Roles = "Customer,Seller")]
    public class OrderController : Controller
    {
        private IRepo<Order> _orderRepo;
        private IRepo<OrderDetail> _orderDetailsRepo;
        private IMapper<OrderDetail, OrderHistoryViewModel> _orderDetailmapper;
        private IMapper<OrderDetail, OrderViewModel> _ordermapper;
        private IRepo<Book> _bookRepo;
        private UserManager<User> _userManager;

        public OrderController(IRepo<Order> orderRepo, IRepo<OrderDetail> orderDetailsRepo, IMapper<OrderDetail, OrderViewModel> ordermapper, IMapper<OrderDetail, OrderHistoryViewModel> orderDetailsmapper, UserManager<User> userManager, IRepo<Book> bookRepo)
        {
            _orderDetailsRepo = orderDetailsRepo;
            _orderDetailmapper = orderDetailsmapper;
            _userManager = userManager;
            _bookRepo = bookRepo;
            _ordermapper = ordermapper;
            _orderRepo = orderRepo;
        }

        [Authorize(Roles = "Customer,Seller")]
        [HttpGet]
        public async Task<IActionResult> OrderHistory()
        {
            List<OrderDetail> details = new List<OrderDetail>();
            var currentUserId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
            if (HttpContext.User.IsInRole("Seller"))
            {
                details = _orderDetailsRepo.Get(x => x.Book.SellerId == currentUserId).ToList();
            }

            if (HttpContext.User.IsInRole("Customer"))
            {
                details = _orderDetailsRepo.Get(x => x.Order.CustomerId == currentUserId).ToList();
            }

            return View(new ListOrderHistoryViewModel() { Orders = _orderDetailmapper.Map(details) });
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Order(long id)
        {
            List<OrderViewModel> list = new List<OrderViewModel>();

            if (HttpContext.Session.GetString("order") != null)
            {
                list = JsonConvert.DeserializeObject<List<OrderViewModel>>(HttpContext.Session.GetString("order"));
            }

            if (id == 0)
            {
                return View(GetModelsFromSession(list));
            }
            else
            {
                var book = await _bookRepo.GetAsync(id);

                OrderDetail orderDetail = new OrderDetail
                {
                    Book = book
                };

                if (!list.Any(x => x.BookId == book.Id))
                {
                    orderDetail.Amount++;
                    list.Add(_ordermapper.Map(orderDetail));
                }
                else
                {
                    list.Where(x => x.BookId == book.Id).ToList().ForEach(s => s.Quantity++);
                }

                return View(GetModelsFromSession(list));
            }
        }

        public ListOrdersViewModel GetModelsFromSession(List<OrderViewModel> list)
        {
            var str = JsonConvert.SerializeObject(list);
            HttpContext.Response.Cookies.Append("Count", list.Count.ToString());
            HttpContext.Session.SetString("order", str);
            ListOrdersViewModel listOrder = new ListOrdersViewModel
            {
                Models = JsonConvert.DeserializeObject<List<OrderViewModel>>(HttpContext.Session.GetString("order"))
            };
            return listOrder;
        }

        public IActionResult RemoveFromOrder(long id)
        {
            List<OrderViewModel> list = new List<OrderViewModel>();
            if (HttpContext.Session.GetString("order") != null)
            {
                list = JsonConvert.DeserializeObject<List<OrderViewModel>>(HttpContext.Session.GetString("order"));
            }

            list.Remove(list.Find(x => x.BookId == id));

            var str = JsonConvert.SerializeObject(list);
            HttpContext.Session.SetString("order", str);

            return RedirectToAction("Order", "Order");
        }

        public IActionResult UpdateOrder(int[] quantity)
        {
            List<OrderViewModel> list = new List<OrderViewModel>();
            if (HttpContext.Session.GetString("order") != null)
            {
                list = JsonConvert.DeserializeObject<List<OrderViewModel>>(HttpContext.Session.GetString("order"));
            }

            int temp = 0;
            foreach (var item in list)
            {
                item.Quantity = quantity[temp];
                temp++;
            }

            var str = JsonConvert.SerializeObject(list);
            HttpContext.Session.SetString("order", str);

            return RedirectToAction("Order", "Order");
        }

        public async Task<IActionResult> SubmitOrder()
        {
            List<OrderViewModel> list = new List<OrderViewModel>();
            list = JsonConvert.DeserializeObject<List<OrderViewModel>>(HttpContext.Session.GetString("order"));
            DateTime date = DateTime.Now;
            User _user = await _userManager.GetUserAsync(HttpContext.User);
            decimal totalPrice = 0;

            foreach (var item in list)
            {
                Book book = await _bookRepo.GetAsync(item.BookId);
                totalPrice += item.TotalPrice;
                book.Amount -= item.Quantity;
            }

            await _orderRepo.CreateAsync(new Order
            {
                StatusId = 1,
                CustomerId = _user.Id,
                AddedDate = date,
                OrderDate = date,
                TotalPrice = totalPrice
            });
            long currOrderId = _orderRepo.Get(x => x.AddedDate == date).FirstOrDefault().Id;
            foreach (var item in list)
            {
                item.OrderId = currOrderId;
                await _orderDetailsRepo.CreateAsync(_ordermapper.Map(item));
            }

            if (list != null)
            {
                HttpContext.Session.Clear();
            }

            return View();
        }
    }
}