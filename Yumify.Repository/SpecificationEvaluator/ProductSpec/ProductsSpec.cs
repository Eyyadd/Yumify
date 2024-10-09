using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yumify.API.Helper.Sorting;
using Yumify.Core.Entities;
using Yumify.Core.Specification;

namespace Yumify.Repository.SpecificationEvaluator.ProductSpec
{
    public class ProductsSpec : BaseSpecification<Product>
    {
        public ProductsSpec(Sorting? sort) : base()
        {
            AddIncludes();
            if (sort is not null)
            {
                var sortby = sort.SortingBy.ToString();

                switch (sortby)
                {
                    case "Name":
                        AssignOrderBy(P => P.Name, sort.IsAscending);
                        break;
                    case "Price":
                        AssignOrderBy(P => P.Price, sort.IsAscending);
                        break;
                    case "Id":
                        AssignOrderBy(P=>P.Id, sort.IsAscending);
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
