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

        public Cart(string Id)
        {
            this.Id = Id;
            Items= new List<CartItems>();
        }
    }
}
