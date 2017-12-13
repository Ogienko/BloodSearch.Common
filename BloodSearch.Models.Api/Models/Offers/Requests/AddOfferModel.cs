using Newtonsoft.Json;

namespace BloodSearch.Models.Api.Models.Offers.Requests {

    public class AddOfferModel {

        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("type")]
        public OfferTypeEnum Type { get; set; }

        [JsonProperty("user-id")]
        public int? UserId { get; set; }

        [JsonProperty("offer")]
        public OfferModel Offer { get; set; }
    }
}