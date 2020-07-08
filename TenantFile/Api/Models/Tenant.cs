using Google.Apis.Http;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TenantFile.Api.Models
{
    [FirestoreData]
    public class Tenant : User
    {
        [FirestoreProperty]
        List<ResidenceRecord?> ResidenceHistory { get; set; } = new List<ResidenceRecord?>();
        [FirestoreProperty]
        public List<string>? Images { get; set; } = new List<string>();

        public Tenant() : base()
        {

        }
      
    }
}
