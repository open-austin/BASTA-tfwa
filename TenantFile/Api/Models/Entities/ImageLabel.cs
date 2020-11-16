namespace TenantFile.Api.Models.Entities
{
    public record ImageLabel(
        
        ///To differentiate between auto-generated labels and Organizer/Tenant labels
        ///When a label is created, this prop needs to be populated with how it was created
        string Source,
        ///A description of what the image is meant to capture
        ///
        string Label,
        ///optional
        ///Used by GPCImageAnnotator
        float? Confidence = null       
        );
}
