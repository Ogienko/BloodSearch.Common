using BloodSearch.Core.Api;
using BloodSearch.Core.Configs;
using BloodSearch.Models.Api.Models.Offers.Requests;
using Newtonsoft.Json;

namespace BloodSearch.Models.Api {

    public class BloodSearchModelsRemoteProvider {

        private const int CacheDurationInMinutes = 15;
        private const string SchemeAndHostConfigName = "BloodSearch.Models.Api.SchemeAndHost";
        private static readonly string SchemeAndHost = ConfigProvider.Get(SchemeAndHostConfigName);

        public static AddOfferResult AddOfferSync(AddOfferModel model) {
            var url = $"{SchemeAndHost}/api/offers/add-offer";
            var jData = JsonConvert.SerializeObject(model);
            return ApiProvider.ExecutePostSync<AddOfferResult>(url, jData);
        }

        public static GetOfferResult GetOffer(long id) {
            var url = $"{SchemeAndHost}/api/offers/get?id={id}";
            var str = ApiProvider.ExecuteGetAsStringSync(url);
            return JsonConvert.DeserializeObject<GetOfferResult>(str);
        }
    }
}