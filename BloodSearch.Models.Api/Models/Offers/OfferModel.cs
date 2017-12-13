using Newtonsoft.Json;

namespace BloodSearch.Models.Api.Models.Offers {

    public class OfferModel {

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("category")]
        public CategoryEnum Category { get; set; }

        [JsonProperty("city")]
        public int City { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("contact")]
        public string Contact { get; set; }
    }
}