using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LvivCompany.Bookstore.DataAccess.Repo;
using LvivCompany.Bookstore.Web.Mapper;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace LvivCompany.Bookstore.Web.Controllers
{
    
    public class OrderController : Controller
    {

        private IRepo<OrderDetail> _orderDetailsRepo;
        private IMapper<OrderDetail, OrderHistoryViewModel> _orderDetailmapper;
        private IRepo<Book> _bookRepo;
        private UserManager<User> _userManager;


        public OrderController(IRepo<OrderDetail> orderDetailsRepo, IMapper<OrderDetail, OrderHistoryViewModel> orderDetailsmapper, UserManager<User> userManager, IRepo<Book> bookRepo)
        {
            _orderDetailsRepo = orderDetailsRepo;
            _orderDetailmapper = orderDetailsmapper;
            _userManager = userManager;
            _bookRepo = bookRepo;
     
        }
        [Authorize(Roles ="Customer,Seller")]
        [HttpGet]
        public async Task<IActionResult> OrderHistory()
        {
            List<OrderDetail> details = new List<OrderDetail>();
            var currentUserId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
            if (HttpContext.User.IsInRole("Seller"))
            {
                details = (_orderDetailsRepo.Get(x => x.Book.SellerId == currentUserId)).ToList(); 

            }
            if (HttpContext.User.IsInRole("Customer"))
            {
               details = (_orderDetailsRepo.Get(x => x.Order.CustomerId == currentUserId)).ToList();

            }

            return View(new ListOrderHistoryViewModel() { orders = _orderDetailmapper.Map(details) });
        }
        [HttpGet]
        [Authorize(Roles ="Customer")]
        public IActionResult Order()
        {
            return View();
        }
    }
}