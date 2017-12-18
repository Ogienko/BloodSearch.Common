using BloodSearch.Core.Models;
using Newtonsoft.Json;
using System;

namespace BloodSearch.Models.Api.Models.Auth.Response {

    public class LoginResult : BaseResponse {

        [JsonProperty]
        public bool IsAuth { get; set; }

        [JsonProperty]
        public string Token { get; set; }

        [JsonProperty]
        public int UserId { get; set; }

        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public string Phone { get; set; }

        [JsonProperty]
        public DateTime Expires { get; set; }
    }
}