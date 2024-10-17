using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumify.Core.Entities.OrdersEntities
{
    public class OrderItems : BaseEntity
    {
        public virtual ProductItemOrder Product {  get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }   // price for the product as item { discounted or not }

        //public Order Order { get; set; } = null!;

        public OrderItems() { }
        public OrderItems(ProductItemOrder product,int quantity , decimal price)
        {
            Product = product;
            Quantity = quantity;
            Price = price;
        }


    }
}
