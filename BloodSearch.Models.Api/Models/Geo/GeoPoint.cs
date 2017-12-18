using Newtonsoft.Json;

namespace BloodSearch.Models.Api.Models.Geo {

    public class GeoPoint {

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lng")]
        public double Lng { get; set; }
    }
}