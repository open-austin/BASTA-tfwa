using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TenantFile.Api.Models;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class OrganizerController : ControllerBase
    {
        private readonly TenantFileContext context;

        public OrganizerController(TenantFileContext context)
        {
            this.context = context;
        }

        [HttpPost("/organizer/create")]
        public async Task<IActionResult> CreateOrganizerAsync([FromQuery] string userUid)
        {
            //await using TenantFileContext dbContext = dbContextFactory.CreateDbContext();
            context.Organizers.Add(new Organizer() {Uid = userUid});

            await context.SaveChangesAsync();

                return StatusCode(201);
          
        }
    }
}