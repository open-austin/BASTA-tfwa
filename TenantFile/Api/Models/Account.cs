using Google.Cloud.Firestore;

namespace TenantFile.Api.Models
{
    public class Account
    {
        public string GUID;
        public string PhoneNumber;
        public string Name;
        public string? Language;

        public Account(DocumentSnapshot snapshot)
        {
            GUID = snapshot.GetValue<string>("GUID");
            PhoneNumber = snapshot.GetValue<string>("PhoneNumber");
            Name = snapshot.GetValue<string>("Name");
            Language = snapshot.GetValue<string>("Language");
        }
    }
}
