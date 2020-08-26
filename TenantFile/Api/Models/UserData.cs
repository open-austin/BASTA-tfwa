using System.Collections.Generic;

namespace TenantFile.Api.Models
{
    public class UserData
    {

        public UserData(string uid, string email, string displayName, Dictionary<string, bool> claims)
        {
            Uid = uid;
            Email = email;
            Claims = claims;
            DisplayName = displayName;
        }

        public string Uid { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public Dictionary<string, bool> Claims { get; set; }
    }
}