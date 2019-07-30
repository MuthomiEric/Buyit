using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buyit.BOL.DTO;
using Buyit.DAL;
using Buyit.DAL.Models.Cart;
using Buyit.DAL.Services.Interfaces;
using Buyit.Models.ViewModels.ShoppingCart;
using Microsoft.AspNetCore.Mvc;

namespace Buyit.Controllers
{
    public class ShoppingCartController : Controller
    {

        private readonly IProductRepository _productRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(IProductRepository productRepository, ShoppingCart shoppingCart)
        {

            _productRepository = productRepository;
            _shoppingCart = shoppingCart;
        }


        public IActionResult Index()
        {
            var item = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = item;

            ShoppingCartVM sc = new ShoppingCartVM
            {
                shoppingCart = _shoppingCart,
                CartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(sc);
        }


        public IActionResult AddToCart(Guid id)
        {
            var selectedItem = _productRepository.GetById(id);

            if (selectedItem != null)
            {
                _shoppingCart.AddToCart(selectedItem, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromCart(Guid id)
        {
            var selectedItem = _productRepository.GetById(id);

            if (selectedItem != null)
            {
                _shoppingCart.RemoveFromCart(selectedItem);
            }
            return RedirectToAction("Index");
        }
    }
}