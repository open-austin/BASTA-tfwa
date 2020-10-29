using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TenantFile.Api.Models
{
    public class OrganizerPhone
    {
        public string OrganizerId { get; set; } = null!;
        public int PhoneId { get; set; }

        public Phone Phone { get; set; } = null!;
        public virtual Organizer Organizer { get; set; } = null!;

    }
}