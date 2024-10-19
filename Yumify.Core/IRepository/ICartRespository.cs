using Yumify.Core.Entities;

namespace Yumify.Core.IRepository
{
    public interface ICartRespository
    {
        Task<Cart?> GetCartAsync(string id);
        Task<Cart?> CreateUpdateCartAsync(Cart cart);
        Task<bool> DeleteCartAsync(string id);
    }
}
