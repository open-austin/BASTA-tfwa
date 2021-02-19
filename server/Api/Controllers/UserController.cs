using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenantFile.Api.Models;

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
        public async Task<UserListResult> ListUsers()
        {
            var pagedEnumerable = FirebaseAuth.DefaultInstance.ListUsersAsync(null);
            var responses = await pagedEnumerable.AsRawResponses().ToListAsync();
            var users = responses
                .SelectMany(x => x.Users)
                .Select(x => new UserData(
                    x.Uid,
                    x.Email,
                    x.DisplayName,
                    x
                        .CustomClaims
                        .Where(claim => claim.Value is bool)
                        .ToDictionary(claim => claim.Key, claim => (bool)claim.Value)));
            return new UserListResult { Users = users };
        }

        [HttpPut("/user/update")]
        public async Task UpdateUser([FromBody] UserData userModifications)
        {
            Console.WriteLine(userModifications.Uid);
            var args = new UserRecordArgs()
            {
                Uid = userModifications.Uid,
                Email = userModifications.Email,
                DisplayName = userModifications.DisplayName,
            };
            await FirebaseAuth.DefaultInstance.UpdateUserAsync(args);

            await FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(userModifications.Uid,
                userModifications.Claims.ToDictionary(x => x.Key, x => x.Value as object));
        }

        [HttpDelete("user/delete")]
        public async Task DeleteUser([FromQuery] string uid)
        {
            Console.WriteLine(uid);
            await FirebaseAuth.DefaultInstance.DeleteUserAsync(uid);
        }

        // Create might not be necessary: https://firebase.google.com/docs/auth/web/manage-users#create_a_user
        // [HttpPut("/user/create")]
        // public async Task<string> CreateUser([FromBody] string userId)
        // {
        //     var user = await FirebaseAuth.DefaultInstance.CreateUserAsync(new UserRecordArgs {

        //     });
        //     return user.Uid;
        // }
    }
}