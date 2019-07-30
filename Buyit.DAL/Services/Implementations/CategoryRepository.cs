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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BuyitDbContext _dbContext;

        public CategoryRepository(BuyitDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Category> AddAsync(Category Entity)
        {
            await _dbContext.Categories.AddAsync(Entity);
            await _dbContext.SaveChangesAsync();

            return await GetByIdAsync(Entity.CategoryId);
        }

        public IEnumerable<Category> AllCategories()
        {
            return _dbContext.Categories;
        }

        public IEnumerable<Category> AllEntities()
        {
            throw new NotImplementedException();
        }

        public async void Delete(Guid Id)
        {
            var item = await GetByIdAsync(Id);
            _dbContext.Categories.Remove(item);
        }

        public Category GetById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<Category> GetByIdAsync(Guid Id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(p => p.CategoryId == Id);
        }

        public void UpDate(Category Entity)
        {
            _dbContext.Categories.Update(Entity);
            _dbContext.SaveChangesAsync();
        }



    }
}
