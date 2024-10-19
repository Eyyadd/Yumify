using Microsoft.EntityFrameworkCore;
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

        public async Task<T?> GetByIdWithSpecAsync(ISpecification<T> spec)
        {
            return await SpecificationEvaluator<T>.GetQuery(_yumifyDb.Set<T>(), spec).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {

            return await _yumifyDb.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await SpecificationEvaluator<T>.GetQuery(_yumifyDb.Set<T>(), spec).ToListAsync();
        }

        public async Task<int> GetCountAsync(ISpecification<T> spec)
        {
            return await _yumifyDb.Set<T>().Where(spec.condition).CountAsync();
            //await SpecificationEvaluator<T>.GetQuery(_yumifyDb.Set<T>(), spec).CountAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _yumifyDb.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T Entity)
            => await _yumifyDb.AddAsync(Entity);
        public void Update(T Entity)
            => _yumifyDb.Update(Entity);
        public void Delete(T Entity)
        {
             _yumifyDb.Remove(Entity);
        }
    }
}
