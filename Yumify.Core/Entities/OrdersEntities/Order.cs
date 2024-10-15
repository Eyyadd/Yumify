using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumify.Core.Entities.OrdersEntities
{
    public class Order : BaseEntity
    {
        public required string CustomerMail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;

        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

        public required OrderAddress ShippingAddress { get; set; }

        public required DeliveryMethod DeliveryMethod { get; set; }

        [ForeignKey(nameof(DeliveryMethod))]
        public int DeliveryMethodId {  get; set; }

        public ICollection<OrderItems> Items { get; set; }=new HashSet<OrderItems>();

        public decimal SubtotalPrice { get; set; }  //price for order without delivery cost

        [NotMapped]
        public decimal TotalPrice => SubtotalPrice+DeliveryMethod.Cost;

        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
