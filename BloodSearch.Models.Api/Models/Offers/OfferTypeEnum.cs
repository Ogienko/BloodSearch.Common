using System.Runtime.Serialization;

namespace BloodSearch.Models.Api.Models.Offers {

    public enum OfferTypeEnum {

        [EnumMember(Value = "donor")]
        Donor = 1,

        [EnumMember(Value = "recipient")]
        Recipient = 2,

        [EnumMember(Value = "any")]
        Any = 3
    }
}