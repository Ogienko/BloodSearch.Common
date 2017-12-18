using BloodSearch.Core.Api;
using BloodSearch.Core.Configs;
using BloodSearch.Core.Models;
using BloodSearch.Models.Api.Models.Auth.Request;
using BloodSearch.Models.Api.Models.Auth.Response;
using BloodSearch.Models.Api.Models.Offers.Requests;
using Newtonsoft.Json;
using System;
using System.Web;

namespace BloodSearch.Models.Api {

    public class BloodSearchModelsRemoteProvider {

        private const string SchemeAndHostConfigName = "BloodSearch.Models.Api.SchemeAndHost";
        private static readonly string SchemeAndHost = ConfigProvider.Get(SchemeAndHostConfigName);

        #region Offers
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

        public static GetOffersResult GetOffers(GetOffersByFiltersParameters parameters) {
            var url = $"{SchemeAndHost}/api/offers/get-offers";
            var jData = JsonConvert.SerializeObject(parameters);
            return ApiProvider.ExecutePostSync<GetOffersResult>(url, jData);
        }
        #endregion

        #region User
        private static string GetToken(HttpContext context) {
            try {
                var model = context.Request.Cookies.Get("token");
                return model == null ? "" : model.Value;
            } catch (Exception ex) {
                return "";
            }
        }

        public static LoginResult Login(HttpContext context, string email, string passwordHash) {
            var url = $"{SchemeAndHost}/api/user/login";
            var model = new LoginModel {
                Email = email,
                PasswordHash = passwordHash,
                Ip = context.Request.UserHostAddress
            };

            var parameters = JsonConvert.SerializeObject(model);
            var result = ApiProvider.ExecutePostSync<LoginResult>(url, parameters);

            if (result.Success) {
                context.Response.Cookies.Add(new HttpCookie("token", result.Token) {
                    Expires = result.Expires
                });
            }

            return result;
        }

        public static BaseResponse Registration(HttpContext context, string email, string passwordHash, string name = null, string phone = null) {
            var url = $"{SchemeAndHost}/api/user/registration";
            var model = new RegistrationModel {
                Email = email,
                PasswordHash = passwordHash,
                RegisterFromIp = context.Request.UserHostAddress,
                Name = name,
                Phone = phone
            };

            var parameters = JsonConvert.SerializeObject(model);
            return ApiProvider.ExecutePostSync<BaseResponse>(url, parameters);
        }

        public static UserResult GetUserByContext(HttpContext context) {
            var url = $"{SchemeAndHost}/api/user/get-user-by-context";
            var model = new AuthRequest { Token = GetToken(context) };
            var parameters = JsonConvert.SerializeObject(model);
            return ApiProvider.ExecutePostSync<UserResult>(url, parameters);
        }

        public static UserResult GetUserById(int userId) {
            var url = $"{SchemeAndHost}/api/user/get-user-by-id";
            var parameters = JsonConvert.SerializeObject(userId);
            return ApiProvider.ExecutePostSync<UserResult>(url, parameters);
        }
        #endregion
    }
}