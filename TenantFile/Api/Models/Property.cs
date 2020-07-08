using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace TenantFile.Api.Models
{
    [FirestoreData]
    public class Property
    {
        [FirestoreProperty]
        public DocumentReference? Management { get; set; }

        [FirestoreProperty(ConverterType = typeof(AddressConverter))]
        public Address? Location { get; set; }
    }
}
