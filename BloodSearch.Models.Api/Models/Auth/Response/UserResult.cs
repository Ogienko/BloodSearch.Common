using BloodSearch.Core.Models;
namespace BloodSearch.Models.Api.Models.Auth.Response {

    public class UserResult : BaseResponse {

        public int Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }
    }
}