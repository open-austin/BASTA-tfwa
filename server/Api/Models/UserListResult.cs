using System.Collections.Generic;

namespace TenantFile.Api.Models
{
    public class UserListResult
    {
        public IEnumerable<UserData>? Users { get; set; }
    }
}