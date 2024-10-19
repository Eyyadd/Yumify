using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumify.Core.Entities;
using Yumify.Core.Specification;

namespace Yumify.Repository.SpecificationEvaluator.ProductSpec
{
    public class ProductCountSpec : BaseSpecification<Product>
    {
        public ProductCountSpec(GetSpecParts? specParts) : base(
            P => (!specParts!.Searching!.BrandID.HasValue || P.BrandId == specParts.Searching.BrandID) &&  //in case of there's an value for brand id (false || true) ==> if false the compiler will go to the next condition to check
                (!specParts.Searching.CategoryId.HasValue || P.CategoryId == specParts.Searching.CategoryId) &&
                (string.IsNullOrEmpty(specParts.Searching.Description) || P.Description.ToLower().Contains(specParts.Searching.Description!)) &&
                (string.IsNullOrEmpty(specParts.Searching.Name) || P.Name.ToLower().Contains(specParts.Searching.Name!))
            )
        { }
    }
}
