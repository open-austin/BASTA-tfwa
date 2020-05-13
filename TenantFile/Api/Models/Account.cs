using Google.Cloud.Firestore;

namespace TenantFile.Api.Models
{
    public class Account
    {
        public string GUID;
        public string PhoneNumber;
        public string Name;
        public string? Language;

        public Account(string guid, string number, string name, string language)
        {
            GUID = guid;
            PhoneNumber = number;
            Name = name;
            Language = language;
        }

        public Account(DocumentSnapshot snapshot):
         this(
                snapshot.GetValue<string>("GUID"),
                snapshot.GetValue<string>("PhoneNumber"),
                snapshot.GetValue<string>("Name"),
                snapshot.GetValue<string>("Language")
            )
        {
        }
    }
}
