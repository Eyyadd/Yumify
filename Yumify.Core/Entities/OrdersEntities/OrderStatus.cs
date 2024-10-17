using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Yumify.Core.Entities.OrdersEntities
{
    public enum OrderStatus
    {
        [EnumMember(Value ="Pending")]
        Pending,

        [EnumMember(Value = "Payment Done")]
        PaymentDone,

        [EnumMember(Value = "PaymentFailed")]
        PaymentFailed
    }
}
