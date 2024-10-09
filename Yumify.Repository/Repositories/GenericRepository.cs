using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumify.Core.Entities;
using Yumify.Core.IRepository;
using Yumify.Core.Specification;
using Yumify.Repository.Data;
using Yumify.Repository.SpecificationEvaluator;

namespace Yumify.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly YumifyDbContext _yumifyDb;

        public GenericRepository(YumifyDbContext yumifyDb)
        {
            _yumifyDb = yumifyDb;
        }

        public async Task<T?> GetByIdWithSpec(ISpecification<T> spec)
        {
            return await SpecificationEvaluator<T>.GetQuery(_yumifyDb.Set<T>(), spec).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            if (typeof(T) == typeof(Product))
                return (IEnumerable<T>) await _yumifyDb.Products.Include(P => P.Brand).Include(P => P.Category).ToListAsync();
            return await _yumifyDb.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllWithSpec(ISpecification<T> spec)
        {
          return await SpecificationEvaluator<T>.GetQuery(_yumifyDb.Set<T>(), spec).ToListAsync();
        }

        public async Task<T?> GetById(int id)
        {
            //if(typeof(T) == typeof(Product))
            //    return await _yumifyDb.Products.Where(P=>P.Id==id)
            //        .Include(P=>P.Brand)
            //        .Include(P=>P.Category)
            //        .FirstOrDefaultAsync() as T;
            return await _yumifyDb.Set<T>().FindAsync(id);
        }
    }
}
