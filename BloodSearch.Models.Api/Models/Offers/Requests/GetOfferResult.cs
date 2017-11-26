using System;

namespace BloodSearch.Models.Api.Models.Offers.Requests {

    public class GetOfferResult {

        public long Id { get; set; }

        public OfferTypeEnum Type { get; set; }

        public int? UserId { get; set; }

        public OfferModel Offer { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public OfferStateEnum State { get; set; }
    }
}