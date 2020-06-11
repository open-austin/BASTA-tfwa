using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantFile.Api.Models
{
    public class Landlord : IPropertyManager
    {
        public Landlord(string name, string phoneNumber, ManagementCompany? company = null)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Company = company;
        }

        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string PhoneNumber { get; set; }
        public Type EntityType { get => this.GetType(); }
        public ManagementCompany? Company { get; set; }
    }
}
