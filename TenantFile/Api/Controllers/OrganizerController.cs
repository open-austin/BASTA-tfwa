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
    public class OrganizerController : ControllerBase
    {
        private readonly TenantFileContext context;

        public OrganizerController(TenantFileContext context)
        {
            this.context = context;
        }

       // [HttpGet("/Organizer/{UserName}")]
        // public IActionResult GetOrganizer(){}

        [HttpPost("/Organizer/create")]
        public async Task<IActionResult> CreateOrganizerAsync([FromQuery] string userUid)
        {
          context.Organizers.Add(new Organizer() {Uid = userUid});

          await context.SaveChangesAsync();

          return StatusCode(201);
          //return CreatedAtAction("organizer", new { id = userUid}, userUid );
        }
    }
}