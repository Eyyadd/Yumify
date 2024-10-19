using Yumify.Core.Entities;
using Yumify.Core.Entities.OrdersEntities;

namespace Yumify.Core.IServices
{
    public interface IStripePaymentService
    {
        Task<Cart> CreateOrUpdatePaymentIntent(string CartId);
        Task<Order?> UpdateOrderStatus(string PAymentIntentId, bool IsPaid);
    }
}
