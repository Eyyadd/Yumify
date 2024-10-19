using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yumify.Core.Entities;
using Yumify.Core.Specification;
using Yumify.Service.SpecificationEvaluator.Sorting;

namespace Yumify.Repository.SpecificationEvaluator.ProductSpec
{
    public class ProductsSpec : BaseSpecification<Product>
    {
        public ProductsSpec(GetSpecParts? specParts) : base(
            P=> (!specParts!.Searching!.BrandID.HasValue || P.BrandId==specParts.Searching.BrandID) &&  //in case of there's an value for brand id (false || true) ==> if false the compiler will go to the next condition to check
                (!specParts.Searching.CategoryId.HasValue || P.CategoryId==specParts.Searching.CategoryId) &&
                (string.IsNullOrEmpty(specParts.Searching.Description)|| P.Description.ToLower().Contains(specParts.Searching.Description!)) &&
                (string.IsNullOrEmpty(specParts.Searching.Name) || P.Name.ToLower().Contains(specParts.Searching.Name!)),
            specParts.PageSize,
            specParts.PageIndex

            )
        {
            AddIncludes();
            if (specParts is not null)
            {
                var sortby = specParts.Sorting?.SortingBy.ToString();

                switch (sortby)
                {
                    case "Name":
                        AssignOrderBy(P => P.Name, specParts!.Sorting!.IsAscending);
                        break;
                    case "Price":
                        AssignOrderBy(P => P.Price, specParts!.Sorting!.IsAscending);
                        break;
                    case "Id":
                        AssignOrderBy(P=>P.Id, specParts!.Sorting!.IsAscending);
                        break;
                }
            }


        }
        public ProductsSpec(int id) : base(p => p.Id == id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            Includes.Add(item => item.Brand);
            Includes.Add(item => item.Category);
        }

        private void AssignOrderBy(Expression<Func<Product, object>> expression, bool IsAsscending)
        {
            if (IsAsscending)
            {
                SortingASC = expression;
            }
            else
            {
                SortingDESC = expression;
            }

        }


    }
}
