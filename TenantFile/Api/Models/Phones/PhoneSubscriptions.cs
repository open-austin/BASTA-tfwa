using HotChocolate;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TenantFile.Api.DataLoader;

namespace TenantFile.Api.Models.Phones
{
    [ExtendObjectType(Name = "Subscription")]
    public class PhoneSubscriptions
    {
        [Subscribe]
        [Topic]
        public Task<Phone> OnNewPhoneReceivedAsync(
            [EventMessage] int phoneId,
            PhoneByIdDataLoader phoineById,
            CancellationToken cancellationToken) =>
            phoineById.LoadAsync(phoneId, cancellationToken);
    }
}
