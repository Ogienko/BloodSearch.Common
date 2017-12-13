using Newtonsoft.Json;

namespace BloodSearch.Core.Models {

    public class PhoneModel {

        [JsonProperty("country-code")]
        public string CountryCode { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        public string FormatedNumber {
            get {
                string number, n = this.Number?.Trim() ?? string.Empty;

                switch (n.Length) {
                    case 7:
                        number = n.Substring(0, 3) + "-" + n.Substring(3, 2) + "-" + n.Substring(5, 2);
                        break;
                    case 6:
                        number = n.Substring(0, 2) + "-" + n.Substring(2, 2) + "-" + n.Substring(4, 2);
                        break;
                    case 5:
                        number = n.Substring(0, 3) + "-" + n.Substring(3, 2);
                        break;
                    case 4:
                        number = n.Substring(0, 2) + "-" + n.Substring(2, 2);
                        break;
                    default:
                        number = n;
                        break;
                }

                return $"+{this.CountryCode?.Trim('+', ' ')} ({this.Code?.Trim()}) {number}";
            }
        }

        public override bool Equals(object obj) => this.EqualsPhone(obj as PhoneModel);

        public bool EqualsPhone(PhoneModel phone) => phone != null && phone.FormatedNumber == this.FormatedNumber;

        public override int GetHashCode() => this.FormatedNumber?.GetHashCode() ?? string.Empty.GetHashCode();
    }
}