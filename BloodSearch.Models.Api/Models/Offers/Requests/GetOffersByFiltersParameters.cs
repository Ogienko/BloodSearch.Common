using BloodSearch.Filter.Models.Offers;

namespace BloodSearch.Models.Api.Models.Offers.Requests {

    public class GetOffersByFiltersParameters {

        public SearchFilter Filter { get; set; }

        public PagingFilter PagingFilter { get; set; }
    }
}