using Buyit.BOL.DTO;
using Buyit.DAL.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyit.DAL.Services.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly BuyitDbContext _dbContext;

        public ProductRepository(BuyitDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> AddAsync(Product Entity)
        {

            await _dbContext.Products.AddAsync(Entity);
            await _dbContext.SaveChangesAsync();

            var item = GetById(Entity.ProductId);

            return item;

        }

        public IEnumerable<Product> AllEntities()
        {

            return _dbContext.Products.Include(p => p.Category);

        }

        public void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Product GetById(Guid Id)
        {
            var prod = _dbContext.Products.Include(c => c.Category)
               .FirstOrDefault(p => p.ProductId == Id);
            return prod;
        }

        public async Task<Product> GetByIdAsync(Guid Id)
        {


            var prod = await _dbContext.Products.Include(c => c.Category)
                .FirstOrDefaultAsync(p => p.ProductId == Id);
            return prod;

        }



        public IEnumerable<Product> PreferedProduct()
        {
            return _dbContext.Products.Where(p => p.IsPrefferedDrink);
        }

        public bool ProductExists(Guid id)
        {

            return _dbContext.Products.Any(e => e.ProductId == id);

        }

        public async void UpDate(Product Entity)
        {
            _dbContext.Update(Entity);
            await _dbContext.SaveChangesAsync();

        }


    }
}
