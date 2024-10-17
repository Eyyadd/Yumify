using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumify.Core.Entities;
using Yumify.Core.Specification;

namespace Yumify.Core.IRepository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAllWithSpec(ISpecification<T> spec);
        Task<T?> GetByIdWithSpec(ISpecification<T> spec);
        Task<int> GetCount(ISpecification<T> spec);
        Task Add(T Entity);
        void Update(T Entity);
        void Delete(int id);
    }
}
