using Newtonsoft.Json;

namespace BloodSearch.Models.Api.Models.Geo {

    public class GeoAddress {

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("point")]
        public GeoPoint Point { get; set; }
    }
}