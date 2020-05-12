using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TenantFile.Api.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class UserController : ControllerBase
    {
        [HttpPut("/users/create-admin")]
        public async Task MakeUserAdmin([FromQuery] string userUid)
        {
            await FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(userUid,
                new Dictionary<string, object>
                {
                    { "admin", true }
                });
        }

        [HttpGet("/users/list")]
        public async Task ListUsers()
        {
            // Start listing users from the beginning, 1000 at a time.
            var pagedEnumerable = FirebaseAuth.DefaultInstance.ListUsersAsync(null);
            var responses = pagedEnumerable.AsRawResponses();
            await foreach (var response in responses)
            {
                foreach (ExportedUserRecord user in response.Users)
                {
                    Console.WriteLine($"User: {user.Uid}");
                }
            }
        }
    }
}