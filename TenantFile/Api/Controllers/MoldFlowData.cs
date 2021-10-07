namespace TenantFile.Api.Controllers
{
    /// <summary>
    /// This is data coming from Twilio. The responses will be more open-ended and 
    /// string values that dont have string db field will be stored as labels on the
    /// image. The image id will be passed back to Twilio after it is created in the 
    /// initial api call then passed back to our server here.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="dateTaken"></param>
    /// <param name="description"></param>
    /// <param name="reported"></param>
    /// <param name="propertyAddressTag"></param>
    public record MoldFlowData(string fromNumber,
                               string firstName,
                               string lastName,
                               string dateTaken,
                               string description,
                               string reported,
                               string imageIds,
                               string propertyAddressTag);
}