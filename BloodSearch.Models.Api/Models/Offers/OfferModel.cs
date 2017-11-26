using Newtonsoft.Json;

namespace BloodSearch.Models.Api.Models.Offers {

    public class OfferModel {

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}