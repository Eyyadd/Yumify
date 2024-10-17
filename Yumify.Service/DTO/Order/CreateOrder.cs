using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumify.Core.Entities.OrdersEntities;

namespace Yumify.Service.DTO.Order
{
    public class CreateOrder
    {
        public string cartId { get; set; }
        public OrderAddressDto orderAddress { get; set; }
        public int deliveryMethodId { get; set; }
    }
}
