using System.Runtime.Serialization;

namespace BloodSearch.Models.Api.Models.Offers {
    
    public enum OfferStateEnum {

        [EnumMember(Value = "published")]
        Published = 1,

        [EnumMember(Value = "deleted")]
        Deleted = 2,

        [EnumMember(Value = "new")]
        New = 3
    }
}