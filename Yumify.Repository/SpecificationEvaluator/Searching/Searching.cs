using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumify.Repository.SpecificationEvaluator.Searching
{
    public class Searching
    {
        public int? BrandID { get; set; }

        public int? CategoryId { get; set; }
        private string? _Description;
        public string? Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value?.ToLower() ?? "";
            }
        }

        private string? _Name;
        public string? Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value.ToLower() ?? "";
            }
        }
    }
}
