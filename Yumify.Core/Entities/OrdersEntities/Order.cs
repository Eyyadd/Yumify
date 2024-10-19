using System.ComponentModel.DataAnnotations.Schema;

namespace Yumify.Core.Entities.OrdersEntities
{
    public class Order : BaseEntity
    {
        public string CustomerMail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;

        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

        public OrderAddress ShippingAddress { get; set; }

        public int? DeliveryMethodId { get; set; }
        public virtual DeliveryMethod? DeliveryMethod { get; set; }

        public virtual ICollection<OrderItems> Items { get; set; }=new HashSet<OrderItems>();

        public decimal SubtotalPrice { get; set; }  //price for order without delivery cost

        [NotMapped]
        public decimal TotalPrice => SubtotalPrice + (DeliveryMethod is null ?0.0m : DeliveryMethod.Cost);

        public string PaymentIntentId { get; set; }
    }
}
