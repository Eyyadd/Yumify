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

            return await _yumifyDb.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllWithSpec(ISpecification<T> spec)
        {
            return await SpecificationEvaluator<T>.GetQuery(_yumifyDb.Set<T>(), spec).ToListAsync();
        }

        public async Task<int> GetCount(ISpecification<T> spec)
        {
            return await _yumifyDb.Set<T>().Where(spec.condition).CountAsync();
            //await SpecificationEvaluator<T>.GetQuery(_yumifyDb.Set<T>(), spec).CountAsync();
        }

        public async Task<T?> GetById(int id)
        {
            return await _yumifyDb.Set<T>().FindAsync(id);
        }

        public async Task Add(T Entity)
            => await _yumifyDb.AddAsync(Entity);
        public void Update(T Entity)
            => _yumifyDb.Update(Entity);
        public void Delete(int id)
        {
            var entity = GetById(id);
             _yumifyDb.Remove(entity);
        }
    }
}
