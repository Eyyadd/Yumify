using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumify.Core.Entities.OrdersEntities;

namespace Yumify.Core.Specification.OrderSpec
{
    public class OrderWithPaymentIntentSpecification :BaseSpecification<Order>
    {
        public OrderWithPaymentIntentSpecification(string? PaymentIntentId) : base(O => O.PaymentIntentId == PaymentIntentId) { }
    }
}
