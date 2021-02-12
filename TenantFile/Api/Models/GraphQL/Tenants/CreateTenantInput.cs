using HotChocolate.Types.Relay;
using TenantFile.Api.Models.Entities;
using TenantFile.Api.Models.Residences;

namespace TenantFile.Api.Models.Tenants
{
    public record CreateTenantInput(
        string Name,
        string PhoneNumber,
        CreateResidenceInput? CurrentResidence,
        [ID(nameof(Residence))] int? ResidenceId //this can be handled by the Residence Input if reconfigured
        ){}
    // {
    //     public CreateTenantInput(string Name,
    //     string PhoneNumber,
    //     CreateResidenceInput? CurrentResidence) : this(Name, PhoneNumber, CurrentResidence, null)
    //     {
    //     }
    //     public CreateTenantInput(string Name,
    //     string PhoneNumber,
    //     [ID(nameof(Residence))] int? ResidenceId) : this(Name, PhoneNumber, null, ResidenceId)
    //     {
    //     }
    //     public void Deconstruct(out string name, out string phoneNumber, out CreateResidenceInput? currentResidence)
    //     {
    //         name = Name;
    //         phoneNumber= PhoneNumber;
    //         currentResidence = CurrentResidence;
    //     }
    //     public void Deconstruct(out string name, out string phoneNumber,[ID(nameof(Residence))] out int? residenceId)
    //     {
    //         name = Name;
    //         phoneNumber= PhoneNumber;
    //         residenceId = ResidenceId;
    //     }
    // }
}
