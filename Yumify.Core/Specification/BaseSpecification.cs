using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yumify.Core.Entities;

namespace Yumify.Core.Specification
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> condition { get; set; } = null!;
        public List<Expression<Func<T,object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> SortingASC { get; set; } = null!;
        public Expression<Func<T, object>> SortingDESC { get; set; } = null!;

        public BaseSpecification()
        {
            //With Includes only
        }
        public BaseSpecification(Expression<Func<T, bool>> condition)
        {
            this.condition = condition;
        }


    }
}
