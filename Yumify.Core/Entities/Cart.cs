using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumify.Core.Entities
{
    public class Cart
    {
        public string Id { get; set; } = null!;
        public List<CartItems> Items { get; set; } = null!;
        public int? deliveryMethodId { get; set; }
        public decimal? ShippingCost { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecrete { get; set; }

        public Cart() { }
        public Cart(string Id)
        {
            this.Id = Id;
            Items= new List<CartItems>();
        }
    }
}
