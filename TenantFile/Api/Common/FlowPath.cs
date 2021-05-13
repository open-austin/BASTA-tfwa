using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TenantFile.Api.Common
{

    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))] 
    public enum FlowPath
    {
        [EnumMember(Value = "wrr")]
        WrittenRepairRequest,

        [EnumMember(Value = "noImg")]
        NoImage,

        [EnumMember(Value = "img")]
        Image,
    }
}