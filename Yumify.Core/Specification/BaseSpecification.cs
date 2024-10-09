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
        public int Skip { get; set; }
        public int Take { get; set; }

        public BaseSpecification()
        {
            //With Includes only
        }
        public BaseSpecification(Expression<Func<T, bool>> condition,int pageSize=1,int pageIndex=1)
        {
            this.condition = condition;
            Skip = (pageIndex-1)*pageSize;
            Take= pageSize;
        }


    }
}
