using Buyit.BOL.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Buyit.DAL.Models.Cart
{
    public class ShoppingCart
    {
        private readonly BuyitDbContext _dbContext;
        public ShoppingCart(BuyitDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string _ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }


        public static ShoppingCart GetCart(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = service.GetService<BuyitDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { _ShoppingCartId = cartId };
        }

        public async void AddToCart(Product product, int amount)
        {
            var shoppingCartItem = _dbContext.ShoppingCartItems.Where(x => x.Product.ProductId == product.ProductId && x.ShoppingCartId == _ShoppingCartId).FirstOrDefault();


            if (shoppingCartItem == null)
            {
                var Shoppingcartitem = new ShoppingCartItem
                {
                    Amount = amount,
                    Product = product,
                    ShoppingCartId = _ShoppingCartId
                };


                await _dbContext.ShoppingCartItems.AddAsync(Shoppingcartitem);

            }
            else
            {
                shoppingCartItem.Amount++;


            }
            _dbContext.SaveChanges();
        }


        public int RemoveFromCart(Product product)
        {
            var shoppingCartItem = _dbContext.ShoppingCartItems.Where(x => x.Product.ProductId == product.ProductId && x.ShoppingCartId == _ShoppingCartId).FirstOrDefault();

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;

                }
                else
                {
                    _dbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }


            }
            _dbContext.SaveChanges();
            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {

            List<ShoppingCartItem> ShoppingCartItemList = _dbContext.ShoppingCartItems.Where(x => x.ShoppingCartId == _ShoppingCartId).Include(x => x.Product).ToList();

            return ShoppingCartItemList;

        }

        public void ClearCart()
        {
            var shoppingCartItem = _dbContext.ShoppingCartItems.Where(x => x.ShoppingCartId == _ShoppingCartId);
            _dbContext.ShoppingCartItems.RemoveRange(shoppingCartItem);
            _dbContext.SaveChanges();


        }

        public decimal GetShoppingCartTotal()
        {

            decimal total = _dbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == _ShoppingCartId).Sum(c => c.Product.Price * c.Amount);


            return total;
        }
    }
}

