using System;
using System.Collections.Generic;
using Google.Cloud.Firestore;
using TenantFile.Api.Models;

public class AddressConverter : IFirestoreConverter<Address>
{
    public object ToFirestore(Address value)
    {
        return
        (City: value.City,
         State: value.State,
         StreetAddress: value.StreetAddress,
         ZipCode: value.PostalCode);
    }

    Address IFirestoreConverter<Address>.FromFirestore(object value)
    {
        if (value is IDictionary<string, object> map)
        {
            // Any exceptions thrown here will be propagated directly. You may wish to be more
            // careful, if you need to control the exact exception thrown used when the
            // data is incomplete or has the wrong type.
            return new Address((string)map["City"], (string)map["State"], (string)map["StreetAddress"], (string)map["ZipCode"]);
        }
        throw new ArgumentException($"Unexpected data: {value.GetType()}");
    }
}
