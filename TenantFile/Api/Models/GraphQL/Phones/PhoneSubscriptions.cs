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
    [ExtendObjectType(Name = "Subscription")]
    public class PhoneSubscriptions
    {
        [Subscribe/*(With = nameof(SubscribeToOnNewPhoneReceivedAsync))*/]
        [Topic]
        public Task<Phone> OnNewPhoneReceived(
            [EventMessage] int phoneId,
            DataLoaderById<Phone> phoineById,
            CancellationToken cancellationToken) =>
            phoineById.LoadAsync(phoneId, cancellationToken);

        public async ValueTask<IAsyncEnumerable<int>> SubscribeToOnNewPhoneReceivedAsync(int phoneId,
            [Service] ITopicEventReceiver eventReceiver, CancellationToken cancellationToken) =>
             (await eventReceiver.SubscribeAsync<string, int>("OnNewPhoneReceived_" + phoneId, cancellationToken)).ReadEventsAsync();
    }
}
