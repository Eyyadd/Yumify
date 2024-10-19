using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumify.Core.Entities.OrdersEntities;

namespace Yumify.Service.DTO.Order
{
    public class OrderReturnDto
    {
        public int Id { get; set; }
        public  string CustomerMail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public string OrderStatus { get; set; } 
        public  OrderAddress ShippingAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal DeliveryMethodCost { get; set; }

        public ICollection<OrderItemsDto> Items { get; set; } = new HashSet<OrderItemsDto>();

        public decimal SubtotalPrice { get; set; }  //price for order without delivery cost

        public decimal TotalPrice { get; set; }
        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
