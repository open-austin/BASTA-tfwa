using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ThumbnailName { get; set; } = null!;
        //public DateTime TimeReceived { get; set; }
        public ImageLabel[]? Labels { get; set; }
    }
}