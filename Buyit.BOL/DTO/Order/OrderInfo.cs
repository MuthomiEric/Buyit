using System;
using System.Collections.Generic;
using System.Text;

namespace Buyit.BOL.DTO.Order
{
    public class OrderInfo
    {
        public int OrderInfoId { get; set; }
        public int OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}
