using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buyit.DAL.Services
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> AllEntities();

        Task<T> GetByIdAsync(Guid Id);
        T GetById(Guid Id);

        Task<T> AddAsync(T Entity);

        void UpDate(T Entity);

        void Delete(Guid Id);
    }
}
