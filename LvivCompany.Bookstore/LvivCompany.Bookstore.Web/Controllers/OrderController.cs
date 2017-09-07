using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LvivCompany.Bookstore.DataAccess.Repo;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.Web.Mapper;
using LvivCompany.Bookstore.Web.ViewModels;
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
        private IMapper<OrderDetail, OrderViewModel> _ordermapper;
        private IRepo<Book> _bookRepo;
        private UserManager<User> _userManager;

        public OrderController(IRepo<OrderDetail> orderDetailsRepo, IMapper<OrderDetail, OrderViewModel> ordermapper, UserManager<User> userManager, IRepo<Book> bookRepo, IRepo<Order> orderRepo)
        {
            _orderDetailsRepo = orderDetailsRepo;
            _userManager = userManager;
            _bookRepo = bookRepo;
            _ordermapper = ordermapper;
            _orderRepo = orderRepo;
        }

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
                    list.Add(_ordermapper.Map(orderDetail));
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

        public async Task<IActionResult> SubmitOrder()
        {
            List<OrderViewModel> list = new List<OrderViewModel>();
            list = JsonConvert.DeserializeObject<List<OrderViewModel>>(HttpContext.Session.GetString("order"));
            DateTime date = DateTime.Now;
            User _user = await _userManager.GetUserAsync(HttpContext.User);
            await _orderRepo.CreateAsync(new Order
            {
                StatusId = 1,
                CustomerId = _user.Id,
                AddedDate = date
            });
            long currOrderId = (await _orderRepo.Get(x => x.AddedDate == date)).FirstOrDefault().Id;
            foreach (var item in list)
            {
                item.OrderId = currOrderId;
                await _orderDetailsRepo.CreateAsync(_ordermapper.Map(item));
            }

            if (list != null)
            {
                HttpContext.Session.Clear();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}