using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumify.Core.Entities;

namespace Yumify.Core.IRepository
{
    public interface ICart
    {
        Task<Cart?> GetCart(string id);
        Task<Cart?> CreateUpdateCart(Cart cart);
        Task<bool> DeleteCart(string id);
    }
}
