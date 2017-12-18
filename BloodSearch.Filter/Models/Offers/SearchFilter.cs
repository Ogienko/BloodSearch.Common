using BloodSearch.Models.Api.Models.Offers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BloodSearch.Filter.Models.Offers {

    public class SearchFilter {

        public enum SortEnum {
            [EnumMember(Value = "default")]
            Default = 1
        }

        [JsonProperty("type")]
        public OfferTypeEnum Type { get; set; } = OfferTypeEnum.Donor;

        [JsonProperty("categories")]
        public List<CategoryEnum> Categories { get; set; } = new List<CategoryEnum>();

        [JsonProperty("itemsIds")]
        public List<long> ItemsIds { get; set; } = new List<long>();

        [JsonProperty("cities")]
        public long Region { get; set; }

        [JsonProperty("sort")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SortEnum Sort { get; set; } = SortEnum.Default;

        [JsonProperty("statuses")]
        public List<OfferStateEnum> Statuses { get; set; } = new List<OfferStateEnum>() { };
    }
}