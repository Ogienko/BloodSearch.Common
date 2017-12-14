using System.Collections.Generic;

namespace BloodSearch.Models.Api.Models.Offers.Requests {

    public class GetOffersResult {

        public long TotalCount { get; set; }

        public List<GetOfferResult> Offers { get; set; } = new List<GetOfferResult>();
    }
}