using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumify.Core.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = null!;
        public int CategoryId {  get; set; }
        public int BrandId {  get; set; }
        public virtual ProductBrand Brand { get; set; }=null!;

        public virtual ProductCategory Category { get; set; } = null!;
    }
}
