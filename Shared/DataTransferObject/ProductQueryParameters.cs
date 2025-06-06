using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject
{
    public class ProductQueryParameters
    {
        private const int DefaultPageSize = 5;
        private const int MaxPageSize = 10;
        private int pageSize;

        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSortingOptions Options { get; set; }
        public string? Search { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get => pageSize; set => pageSize = value > 0 && value < MaxPageSize ? value : DefaultPageSize; }
    }
}
