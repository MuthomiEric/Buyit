using Buyit.DAL.Models.Cart;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buyit.ViewComponents
{
    public class ShoppingCartVC : ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartVC(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            ///* var items = new List<Shopp*/ingCartItem> { new ShoppingCartItem(), new ShoppingCartItem() };
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            var shoppingCartVM = new Models.ViewModels.ShoppingCart.ShoppingCartVM
            {
                shoppingCart = _shoppingCart,
                CartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(shoppingCartVM);
        }
    }
}
