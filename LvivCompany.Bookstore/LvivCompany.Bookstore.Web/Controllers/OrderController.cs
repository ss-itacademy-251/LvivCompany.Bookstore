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
        private IRepo<OrderDetail> _orderDetailsRepo;
        private IMapper<OrderDetail, OrderViewModel> _ordermapper;
        private IRepo<Book> _bookRepo;
        private UserManager<User> _userManager;

        public OrderController(IRepo<OrderDetail> orderDetailsRepo, IMapper<OrderDetail, OrderViewModel> ordermapper, UserManager<User> userManager, IRepo<Book> bookRepo)
        {
            _orderDetailsRepo = orderDetailsRepo;
            _userManager = userManager;
            _bookRepo = bookRepo;
            _ordermapper = ordermapper;
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
    }
}