using BloodSearch.Core.Models;

namespace BloodSearch.Models.Api.Models.Auth.Request {

    public class EditUserRequest : AuthRequest {

        public int UserId { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }
    }
}