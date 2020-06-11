using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantFile.Api.Models
{
    public class PhoneNumber
    {
        // ^\(*\+*[1-9]{0,3}\)*-*[1-9]{0,3}[-. /]*\(*[2-9]\d{2}\)*[-. /]*\d{3}[-. /]*\d{4} *e*x*t*\.* *\d{0,4}$
        public string DisplayNumber { get; }

        //private readonly string? _countryCode;
        //private readonly string _areaCode;
        //private readonly string _baseNumber;
        //private readonly string? _extension;
        
        public PhoneNumber(string twillioNumber)
        {
            DisplayNumber = twillioNumber;

        }
        public PhoneNumber(string areaCode, string exchangeNumber, string subscriberNumber, string? countryCode = null, string? extension = null)
        {
           
            DisplayNumber = $"+{countryCode ?? "1"} {areaCode}-{exchangeNumber}-{subscriberNumber} {extension !?? $"ext {extension}"} ";
        }
    }
}
