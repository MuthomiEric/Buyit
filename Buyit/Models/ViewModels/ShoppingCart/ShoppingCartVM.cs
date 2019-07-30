using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Buyit.Models.ViewModels.ShoppingCart
{
    public class ShoppingCartVM
    {
        public Buyit.DAL.Models.Cart.ShoppingCart shoppingCart { get; set; }
        public decimal CartTotal { get; set; }
    }
}
