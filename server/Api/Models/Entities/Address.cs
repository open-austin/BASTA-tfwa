using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using TenantFile.Api.Common;

namespace TenantFile.Api.Models.Entities
{
    public class Address : IEntity
    {
        
        public int Id { get; set; }
     
        [XmlElement("Address2")]
        public string Line1 { get; set; } = null!;
        [XmlElement("Address1")]
        public string? Line2 { get; set; }
        public string? Line3 { get; set; }
        public string? Line4 { get; set; }
        [XmlElement("City")]
        public string City { get; set; } = null!;
        [XmlElement("State")]
        public string State { get; set; } = null!;
        [XmlElement("Zip5")]
        public string PostalCode { get; set; } = null!;

        [XmlElement("ReturnText")]
        public string? ValidationMessage { get; set; }
    }
}