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
using TenantFile.Api.Models.Addresses;

namespace TenantFile.Api.Services
{
    [XmlRoot("AddressValidateResponse")]
    public class AddressValidateResponse
    {
        [XmlElement("Address")]
        public USPSVerifyPayload Address { get; set; } = null!;
    }

    public class USPSVerifyPayload
    {
        [XmlElement(ElementName = "Address2")]
        public string? Line1 { get; set; }
        [XmlElement(ElementName = "Address1")]
        public string? Line2 { get; set; }
        // [XmlElement(ElementName = "Address3")]
        // public string Line3 { get; set; }
        // [XmlElement(ElementName = "Address4")]
        // public string Line4 { get; set; }
        [XmlElement(ElementName = "City")]
        public string? City { get; set; }
        [XmlElement(ElementName = "State")]
        public string? State { get; set; }
        [XmlElement(ElementName = "Zip5")]
        public string? PostalCode { get; set; }
        [XmlElement("ReturnText")]
        public string? ReturnText { get; set; }
        [XmlElement("Error")]
        public USPSError? USPSError { get; set; }
    }

    public class USPSError
    { 
        [XmlElement("Number")]
        public string? Number { get; set; }
        [XmlElement("Description")]
        public string? Description { get; set; }
    }

    public interface IAddressVerificationService
    {
        Task<USPSVerifyPayload> VerifyAddressAsync(VerifyAddressInput address);
    }

    public class AddressVerificationService : IAddressVerificationService
    {
        public AddressVerificationService(string userName)
        {
            this.userName = userName;
        }
        const string URIBASE = @"https://secure.shippingapis.com/ShippingAPI.dll";
        private readonly string userName;

        public async Task<USPSVerifyPayload> VerifyAddressAsync(VerifyAddressInput address)
        {
            var serializer = new XmlSerializer(typeof(AddressValidateResponse));
            var client = new HttpClient();

            //TODO: Can you serialize this instead of interpolating it?
            var body = new Dictionary<string, string>
            {
                { "API", "Verify"},
                { "XML", $@"<AddressValidateRequest USERID = ""{userName}""><Address ID = ""0""><Address1>{address.Line2}</Address1><Address2>{address.Line1}</Address2><City>{address.City}</City><State>{address.State}</State><Zip5>{address.PostalCode}</Zip5><Zip4></Zip4></Address></AddressValidateRequest>"}
            };


            var content = new FormUrlEncodedContent(body.AsEnumerable()!);

            var response = await client.PostAsync(URIBASE, content);

            AddressValidateResponse addressSerial = new AddressValidateResponse();
            try
            {
            var stream = await response.EnsureSuccessStatusCode().Content.ReadAsStreamAsync();
            addressSerial = (AddressValidateResponse)serializer.Deserialize(stream)!;
            //ValdationMessage is null when address is fully successful but if more info is needed, it has a value
            //addressSerial.Address.ValidationMessage = $"Status Code: {response.StatusCode} | Reason Phrase: {response.ReasonPhrase} ";
            return addressSerial.Address!;
            }

            catch (HttpRequestException)
            {
                return new USPSVerifyPayload()
                {
                    ReturnText = $"{addressSerial.Address.ReturnText ?? "Could not validate address at this time"}"
                };
            }
        }
    }
}
