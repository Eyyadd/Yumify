using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yumify.Core.Entities;

namespace Yumify.Core.Specification
{
    public interface ISpecification<T> where T : BaseEntity
    {
        Expression<Func<T,bool>> condition { get; set; }
        List<Expression<Func<T, object>>> Includes { get; set; }
        Expression<Func<T,object>> SortingASC { get; set; }
        Expression<Func<T,object>> SortingDESC { get; set; }
    }
}
