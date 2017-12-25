using Newtonsoft.Json;

namespace BloodSearch.Models.Api.Models.Offers.Requests {

    public class DeleteOfferModel {

        [JsonProperty("offer-id")]
        public long OfferId { get; set; }

        [JsonProperty("user-id")]
        public int UserId { get; set; }

        [JsonProperty("is-admin")]
        public bool IsAdmin { get; set; }
    }
}