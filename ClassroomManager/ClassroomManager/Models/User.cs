using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassroomManager.Models
{
    public class UserAuthentification
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class User
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        public string Password { get; set; }

        [JsonProperty("token")]
        public string AccessToken { get; set; }
    }
}
