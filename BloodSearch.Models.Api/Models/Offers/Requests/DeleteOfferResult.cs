using Newtonsoft.Json;
using System.Collections.Generic;

namespace BloodSearch.Models.Api.Models.Offers.Requests {

    public class DeleteOfferResult {

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("err-messages")]
        public List<KeyValuePair<string, string>> ErrMessages { get; set; } = new List<KeyValuePair<string, string>>();
    }
}