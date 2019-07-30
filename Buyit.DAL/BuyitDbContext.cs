using Buyit.BOL.DTO;
using Buyit.BOL.DTO.Order;
using Buyit.BOL.DTO.Users;
using Buyit.DAL.Models.Cart;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Buyit.DAL
{
    public class BuyitDbContext : IdentityDbContext<User>
    {
        public BuyitDbContext(DbContextOptions<BuyitDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderInfo> OrderDetails { get; set; }

    }
}
