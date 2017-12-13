using System.Runtime.Serialization;

namespace BloodSearch.Models.Api.Models.Offers {

    public enum CategoryEnum {

        [EnumMember(Value = "first-negative")]
        FirstNegative = 1,

        [EnumMember(Value = "first-positive")]
        FirstPositive = 2,

        [EnumMember(Value = "second-negative")]
        SecondNegative = 3,

        [EnumMember(Value = "second-positive")]
        SecondPositive = 4,

        [EnumMember(Value = "third-negative")]
        ThirdNegative = 5,

        [EnumMember(Value = "third-positive")]
        ThirdPositive = 6,

        [EnumMember(Value = "fourth-negative")]
        FourthNegative = 7,

        [EnumMember(Value = "fourth-positive")]
        FourthPositive = 8
    }
}