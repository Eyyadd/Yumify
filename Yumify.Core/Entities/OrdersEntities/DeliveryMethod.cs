using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumify.Core.Entities.OrdersEntities
{
    public class DeliveryMethod:BaseEntity
    {

        //[Key]
        //public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string DeliveryTime { get; set; }
        public decimal Cost { get; set; }

        public ICollection<Order?> Orders { get; set; }

    }
}
