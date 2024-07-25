using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.Specifications.CustomerSpecifications
{
    public class CustomerSpecParams
    {
        private string? search;
        public string? Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }


        private const int MaxPageSize = 10;
        private int pageSize = 5;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }
        public int PageIndex { get; set; } = 1;
        public string? sort { get; set; }
        
    }
}
