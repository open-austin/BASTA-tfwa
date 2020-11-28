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
        private readonly IDbContextFactory<TenantFileContext> dbContextFactory;

        public OrganizerController(IDbContextFactory<TenantFileContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

       // [HttpGet("/Organizer/{UserName}")]
        // public IActionResult GetOrganizer(){}

        [HttpPost("/organizer/create")]
        public async Task<IActionResult> CreateOrganizerAsync([FromQuery] string userUid)
        {
            await using TenantFileContext dbContext = dbContextFactory.CreateDbContext();
            dbContext.Organizers.Add(new Organizer() {Uid = userUid});

            await dbContext.SaveChangesAsync();

                return StatusCode(201);
          //return CreatedAtAction("organizer", new { id = userUid}, userUid );
        }
    }
}