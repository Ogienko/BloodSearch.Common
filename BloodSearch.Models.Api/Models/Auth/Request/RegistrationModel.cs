using System;

namespace BloodSearch.Models.Api.Models.Auth.Request {

    public class RegistrationModel {

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string RegisterFromIp { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }
    }
}