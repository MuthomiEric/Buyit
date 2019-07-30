using Buyit.BOL.DTO.Order;
using Buyit.BOL.DTO.Users;
using Buyit.DAL.Models.Cart;
using Buyit.DAL.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyit.DAL.Services.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BuyitDbContext _dbContext;
        private readonly ShoppingCart _shoppingCart;


        public OrderRepository(BuyitDbContext dbContext, ShoppingCart shoppingCart)
        {
            _dbContext = dbContext;
            _shoppingCart = shoppingCart;

        }
        public void CreateOrder(Order order)
        {
            order.PlacedAt = DateTime.Now;
            _dbContext.Orders.Add(order);
            var cartItems = _shoppingCart.ShoppingCartItems;

            foreach (var item in cartItems)
            {
                var orderInfo = new OrderInfo
                {
                    Amount = item.Amount,
                    OrderId = order.OrderId,
                    ProductId = item.Product.ProductId,
                    Price = item.Product.Price

                };

                _dbContext.OrderDetails.Add(orderInfo);
                _dbContext.SaveChanges();
            }

        }

        public async Task<List<OrderInfo>> OrdersByUserAsync(User user)
        {
            List<OrderInfo> orderItems = null;

            var orders = await _dbContext.Orders.Where(u => u.User.Id == user.Id).ToListAsync();
            foreach (var order in orders)
            {
                var items = await _dbContext.OrderDetails.Where(p => p.OrderId == order.OrderId).ToListAsync();
                orderItems = items;
            }
            return orderItems;
        }


    }
}
