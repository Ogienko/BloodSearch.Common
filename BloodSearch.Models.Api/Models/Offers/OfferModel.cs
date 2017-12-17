using BloodSearch.Core.Models;
using BloodSearch.Models.Api.Models.Geo;
using Newtonsoft.Json;
using System;

namespace BloodSearch.Models.Api.Models.Offers {

    public class OfferModel {

        [JsonProperty("contact-name")]
        public string ContactName { get; set; }

        [JsonProperty("category")]
        public CategoryEnum Category { get; set; }

        [JsonProperty("date-of-birth")]
        public DateTime DateOfBirth { get; set; }

        [JsonProperty("weight")]
        public int Weight { get; set; }

        [JsonProperty("healthy")]
        public bool Healthy { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("phone")]
        public PhoneModel Phone { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("geo-coder-meta")]
        public GeoCoderMeta GeoCoderMeta { get; set; }
    }
}