using System.Runtime.Serialization;

namespace BloodSearch.Models.Api.Models.Offers {

    public enum CategoryEnum {

        [EnumMember(Value = "O(I) Rh−")]
        FirstNegative = 1,

        [EnumMember(Value = "O(I) Rh+")]
        FirstPositive = 2,

        [EnumMember(Value = "A(II) Rh−")]
        SecondNegative = 3,

        [EnumMember(Value = "A(II) Rh+")]
        SecondPositive = 4,

        [EnumMember(Value = "B(III) Rh−")]
        ThirdNegative = 5,

        [EnumMember(Value = "B(III) Rh+")]
        ThirdPositive = 6,

        [EnumMember(Value = "AB(IV) Rh−")]
        FourthNegative = 7,

        [EnumMember(Value = "AB(IV) Rh+")]
        FourthPositive = 8
    }
}