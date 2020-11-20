using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Extensions.Configuration;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Services
{
    [XmlRoot("AddressValidateResponse")]
    public class AddressValidateResponse
    {
        [XmlElement("Address")]
        public Address Address { get; set; } = null!;
     
    }

    public interface IAddressVerificationService
    {
        Task<Address?> VerifyAddressAsync(Address address);
    }

    public class AddressVerificationService : IAddressVerificationService
    {
        public AddressVerificationService(string userName)
        {
            this.userName = userName;
        }
        public IConfiguration Configuration { get; } = null!;

        const string URIBASE = @"https://secure.shippingapis.com/ShippingAPI.dll";
        private readonly string userName;

        public async Task<Address?> VerifyAddressAsync(Address address)
        {
            var client = new HttpClient();

            //TODO: Can you serialize this instead of interpolating it?
            var body = new Dictionary<string, string>
            {
                { "API", "Verify"},
                { "XML", $@"<AddressValidateRequest USERID = ""{userName}""><Address ID = ""0""><Address1>{address.Line2}</Address1><Address2>{address.Line1}</Address2><City>{address.City}</City><State>{address.State}</State><Zip5>{address.PostalCode}</Zip5><Zip4></Zip4></Address></AddressValidateRequest>"}
            };

            var serializer = new XmlSerializer(typeof(AddressValidateResponse));

            var content = new FormUrlEncodedContent(body!);

            var response = await client.PostAsync(URIBASE, content);

            AddressValidateResponse addressSerial = new AddressValidateResponse();
            try
            {
                var stream = await response.EnsureSuccessStatusCode().Content.ReadAsStreamAsync();
                addressSerial = (AddressValidateResponse)serializer.Deserialize(stream)!;
                //ValdationMessag is null when address is fully successful but if more info is needed, it have a value
                //addressSerial.Address.ValidationMessage = $"Status Code: {response.StatusCode} | Reason Phrase: {response.ReasonPhrase} ";
                return addressSerial.Address;
            }

            catch (HttpRequestException)
            {
                return new Address() { ValidationMessage = $"{addressSerial.Address.ValidationMessage ?? "Could not validate address at this time"}" };
            }



        }
    }
}
