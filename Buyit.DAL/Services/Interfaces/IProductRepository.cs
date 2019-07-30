using Buyit.BOL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buyit.DAL.Services.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        IEnumerable<Product> PreferedProduct();
        bool ProductExists(Guid id);

    }
}
