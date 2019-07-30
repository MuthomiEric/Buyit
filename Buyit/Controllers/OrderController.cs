using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buyit.BOL.DTO.Order;
using Buyit.BOL.DTO.Users;
using Buyit.DAL.Models.Cart;
using Buyit.DAL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Buyit.Controllers
{

    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCart _shoppingCart;
        private readonly UserManager<User> _userManager;


        public OrderController(IOrderRepository orderRepository,
            ShoppingCart shoppingCart,
            UserManager<User> userManager)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
            _userManager = userManager;


        }



        public IActionResult UserCheck()
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            User appUser = _userManager.FindByIdAsync(userId).Result;
            return View(appUser);
        }

        public IActionResult CheckOut()
        {

            return View();
        }

        [HttpPost]
        public IActionResult CheckOut(Order order)
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            User appUser = _userManager.FindByIdAsync(userId).Result;

            order.User = appUser;

            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;


            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your Cart Is Empty, Add Some Products in your Cart first");
            }

            if (ModelState.IsValid)
            {
                _orderRepository.CreateOrder(order);
                _shoppingCart.ClearCart();
                return RedirectToAction("OrderComplete");
            }
            return View(order);
        }

        public IActionResult OrderComplete()
        {

            ViewBag.Message = "Thank You For shopping with Us, Welcome Again";
            return View();
        }
    }
}