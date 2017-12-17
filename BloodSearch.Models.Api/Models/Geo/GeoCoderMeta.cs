using Newtonsoft.Json;

namespace BloodSearch.Models.Api.Models.Geo {

    public class GeoCoderMeta {

        [JsonProperty("cuntry_code")]
        public string CountryCode { get; set; }

        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }

        [JsonProperty("precision")]
        public string Precision { get; set; }

        [JsonProperty("point")]
        public GeoPoint Point { get; set; }
    }
}