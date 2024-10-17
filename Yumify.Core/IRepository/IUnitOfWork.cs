using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumify.Core.Entities;

namespace Yumify.Core.IRepository
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<T> Myrepository<T>() where T : BaseEntity;
        Task<int> CompleteAsync();


    }
}
