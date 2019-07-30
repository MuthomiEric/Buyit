using Buyit.BOL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buyit.DAL.Services.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        IEnumerable<Category> AllCategories();
    }
}
