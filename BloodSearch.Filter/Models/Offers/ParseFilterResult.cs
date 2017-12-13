using System;

namespace BloodSearch.Filter.Models.Offers {

    public class ParseFilterResult {

        public SearchFilter Filter { get; set; } = new SearchFilter();

        public PagingFilter PagingFilter { get; set; } = new PagingFilter();
    }
}