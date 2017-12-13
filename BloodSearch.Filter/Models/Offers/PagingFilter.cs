using System;

namespace BloodSearch.Filter.Models.Offers {

    public class PagingFilter {

        public const int DefaultPageSize = 20;

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = DefaultPageSize;

        public int MaxPageNumber { get; set; }
    }
}