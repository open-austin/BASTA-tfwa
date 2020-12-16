using TenantFile.Api.Models.Residences;

namespace TenantFile.Api.Models.Tenants
{
  public record CreateTenantInput(
      string Name,
      string PhoneNumber,
      CreateResidenceInput? CurrentResidence
      );
}
