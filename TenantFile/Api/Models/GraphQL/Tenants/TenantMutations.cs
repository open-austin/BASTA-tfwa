using HotChocolate;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TenantFile.Api.Extensions;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Tenants
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    public class TenantMutations
    {
        [UseTenantFileContext]
        public async Task<CreateTenantPayload> CreateTenantAsync(
            CreateTenantInput inputTenant,
            [ScopedService] TenantFileContext context,
            CancellationToken cancellationToken)
        {
            var (nameInput, phoneInput, residenceInput, residenceIdInput) = inputTenant;
            // See if the phone number exists already
            Phone? phone = context.Phones.FirstOrDefault(x => x.PhoneNumber == inputTenant.PhoneNumber);

            // If it doesn't exist, create a new phone number
            if (phone == null)
            {
                phone = new Phone
                {
                    PhoneNumber = phoneInput
                };
            }
            var tenant = new Tenant
            {
                Name = nameInput,
                Phones = new List<Phone> {
                    phone
                },
            };

            if (residenceInput != null)
            {
                var (line1, line2, line3, line4, city, state, postalCode) = residenceInput!.AddressInput;
                tenant.CurrentResidence = new Residence()
                {
                    Address = new Address()
                    {
                        Line1 = line1,
                        Line2 = line2,
                        Line3 = line3,
                        Line4 = line4,
                        City = city,
                        State = state,
                        PostalCode = postalCode

                    },
                    PropertyId = residenceInput.PropertyId

                };
            }
            else
            {
                tenant.ResidenceId = residenceIdInput!;
            }

            var tenantEntry = context.Tenants.Update(tenant);
            await context.SaveChangesAsync(cancellationToken);
            return new CreateTenantPayload(tenant);
        }
        [UseTenantFileContext]
        public async Task<EditTenantPayload> EditTenantAsync(
           EditTenantInput inputTenant,
           [ScopedService] TenantFileContext context,
           CancellationToken cancellationToken)
        {
            // var (nameInput, phoneInput, residenceInput, residenceIdInput) = inputTenant;

            Tenant? tenant = await context.Tenants.FirstOrDefaultAsync(x => x.Id == inputTenant.TenantId);
            Phone phone = await context.Phones.FirstOrDefaultAsync(x => x.PhoneNumber == inputTenant.PhoneNumber) ?? new Phone() { PhoneNumber = inputTenant.PhoneNumber };
            // If it doesn't exist, create a new tenant number
            if (tenant == null)
            {
                tenant = new Tenant
                {
                    Name = inputTenant.Name,
                    Phones = new List<Phone> {
                    phone
                }
                };

            }
            else
            {
                tenant.Name = inputTenant.Name;
            }
            if (inputTenant.CurrentResidence != null)
            {
                var (line1, line2, line3, line4, city, state, postalCode) = inputTenant.CurrentResidence!.AddressInput;
                tenant.CurrentResidence = new Residence()
                {
                    Address = new Address()
                    {
                        Line1 = line1,
                        Line2 = line2,
                        Line3 = line3,
                        Line4 = line4,
                        City = city,
                        State = state,
                        PostalCode = postalCode

                    },
                    PropertyId = inputTenant.CurrentResidence.PropertyId

                };
            }
            else
            {
                tenant.ResidenceId = inputTenant.ResidenceId!;
            }

            await context.SaveChangesAsync(cancellationToken);
            return new EditTenantPayload(tenant);
        }

    }
}

