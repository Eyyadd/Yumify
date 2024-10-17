using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumify.Core.Entities.OrdersEntities;

namespace Yumify.Core.IServices
{
    public interface IOrderServices
    {
        Task<Order> CreateOrderAsync(string CutomerMail, string CartId,int deliveryMethodId,OrderAddress orderAddress);
        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string CutomerMail);
        Task<Order> GetOrderByIdForUserAsync(int OrderId, string CutomerMail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync();
    }
}
