using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumify.Repository.SpecificationEvaluator.Searching;
using Yumify.Service.SpecificationEvaluator.Sorting;

namespace Yumify.Repository.SpecificationEvaluator
{
    public class GetSpecParts
    {
        public Sorting? Sorting { get; set; } = new Sorting()
        {
            IsAscending = true,
            SortingBy = SortingBy.Id
        };

        public Searching.Searching? Searching { get; set; } =new Searching.Searching { Description="",Name=""};
        public int PageSize { get; set; } = 5;
        public int PageIndex { get; set; } = 1;

    }
}
