using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumify.Core.Entities.OrdersEntities
{
    public class ProductItemOrder
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public required string PictreUrl { get; set; } 
    }
}
