using Buyit.BOL.DTO.Order;
using Buyit.BOL.DTO.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buyit.DAL.Services.Interfaces
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
        Task<List<OrderInfo>> OrdersByUserAsync(User user);
    }
}
