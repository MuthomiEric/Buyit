using Buyit.BOL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buyit.DAL.Models.Cart
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }
        public int Amount { get; set; }
        public Product Product { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
