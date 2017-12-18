using System;

namespace BloodSearch.Models.Api.Models.Auth.Request {

    public class LoginModel {

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string Ip { get; set; }
    }
}