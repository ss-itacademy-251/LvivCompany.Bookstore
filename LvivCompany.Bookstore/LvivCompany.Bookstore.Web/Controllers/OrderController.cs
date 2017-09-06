using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LvivCompany.Bookstore.BusinessLogic.Mapper;
using LvivCompany.Bookstore.BusinessLogic.ViewModels;
using LvivCompany.Bookstore.DataAccess.Repo;
using LvivCompany.Bookstore.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.Web.ViewModels;
using LvivCompany.Bookstore.DataAccess.Repo;
using LvivCompany.Bookstore.Web.Mapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LvivCompany.Bookstore.Web.Controllers
{
    [Authorize(Roles = "Customer,Seller")]
    public class OrderController : Controller
    {
        private IRepo<OrderDetail> _orderDetailsRepo;
        private IMapper<OrderDetail, OrderHistoryViewModel> _orderDetailmapper;
        private IMapper<OrderDetail, OrderViewModel> _ordermapper;
        private IRepo<Book> _bookRepo;
        private UserManager<User> _userManager;

        public OrderController(IRepo<OrderDetail> orderDetailsRepo, IMapper<OrderDetail, OrderViewModel> _ordermapper, IMapper<OrderDetail, OrderHistoryViewModel> orderDetailsmapper, UserManager<User> userManager, IRepo<Book> bookRepo)
        {
            _orderDetailsRepo = orderDetailsRepo;
            _orderDetailmapper = orderDetailsmapper;
            _userManager = userManager;
            _bookRepo = bookRepo;
            _ordermapper = ordermapper;
        }

        [Authorize(Roles ="Customer,Seller")]
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
        [Authorize(Roles ="Customer")]
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
    }
}