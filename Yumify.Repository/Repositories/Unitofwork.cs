using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumify.Core.Entities;
using Yumify.Core.IRepository;
using Yumify.Repository.Data;

namespace Yumify.Repository.Repositories
{
    public class Unitofwork : IUnitOfWork
    {
        private readonly YumifyDbContext _dbContext;
        public Hashtable _Repos = new Hashtable();

        public Unitofwork(YumifyDbContext yumifyDb)
        {
            _dbContext = yumifyDb;
        }

        public IGenericRepository<T> Myrepository<T>() where T : BaseEntity 
        {
            var key =typeof(T).Name;
            if (!_Repos.ContainsKey(key))
            {
                var rep= new GenericRepository<T>(_dbContext) ;
                _Repos.Add(key, rep);
            }
            return _Repos[key] as IGenericRepository<T>;
        }
        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }

        public async Task<int> CompleteAsync()
        {
           return await _dbContext.SaveChangesAsync();
        }
    }
}
