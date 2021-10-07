using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TenantFile.Api.DataLoader;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Phones
{
    [ExtendObjectType(OperationTypeNames.Subscription)]
    public class PhoneSubscriptions
    {
        [Subscribe/*(With = nameof(SubscribeToOnNewPhoneReceivedAsync))*/]
        [Topic]
        public Task<Phone> OnNewPhoneReceived(
            [EventMessage] int phoneId,
            PhoneByIdDataLoader phoineById,
            CancellationToken cancellationToken) =>
            phoineById.LoadAsync(phoneId, cancellationToken);
        //TODO: This is not used currently but if custom implementation is needed
        //public async ValueTask<IAsyncEnumerable<int>> SubscribeToOnNewPhoneReceivedAsync(int phoneId,
        //    [Service] ITopicEventReceiver eventReceiver, CancellationToken cancellationToken) =>
        //     (await eventReceiver.SubscribeAsync<string, int>("OnNewPhoneReceived_" + phoneId, cancellationToken)).ReadEventsAsync();
    }
}
