using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumify.Core.Entities;
using Yumify.Core.Specification;

namespace Yumify.Repository.SpecificationEvaluator
{
    public static class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> dbset, ISpecification<T> spec)
        {
            var query = dbset;
            //if (spec.condition is not null)
            //    query = query.Where(spec.condition);
            //if (spec.SortingASC is not null)
            //{
            //    query = query.OrderBy(spec.SortingASC);
            //}
            //else if (spec.SortingDESC is not null)
            //{
            //    query = query.OrderByDescending(spec.SortingDESC);
            //}
            //if (spec.Includes?.Count() > 0)
            //{
            //    query = spec.Includes.Aggregate(query, (currentQry, includeExp) => currentQry.Include(includeExp));
            //}
            query= spec.condition is not null ? query.Where(spec.condition) : query;
            query = spec.SortingASC is not null ? query.OrderBy(spec.SortingASC) : query;
            query= spec.SortingDESC is not null ? query.OrderByDescending(spec.SortingDESC) : query;
            query = query.Skip(spec.Skip).Take(spec.Take);
            query = spec.Includes?.Count() > 0 ? spec.Includes.Aggregate(query, (currentQry, includeExp) => currentQry.Include(includeExp)) : query;

            return query;
        }
    }
}
