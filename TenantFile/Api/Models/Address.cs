using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace TenantFile.Api.Models
{
    //[FirestoreData]
    public class Address
    {
        //[FirestoreProperty("StreetAdress")]
        public string StreetAddress { get; set; }
        //[FirestoreProperty]
        //public string AddressLine2 { get; set; }

        //[FirestoreProperty]
        public string City { get; set; }

        //[FirestoreProperty]
        public string State { get; set; }

        //[FirestoreProperty]
        public string PostalCode { get; set; }
        public string? Building { get; set; }


        public Address(string city, string addressLine1, /*string addressLine2,*/ string state, string postalCode, string? building = null)
        {
            StreetAddress = addressLine1;
            //AddressLine2 = addressLine2;
            City = city;
            State = state;
            PostalCode = postalCode;
            Building = building;
        }
    }
}
