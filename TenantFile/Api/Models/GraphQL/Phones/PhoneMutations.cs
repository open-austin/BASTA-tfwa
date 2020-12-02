using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using TenantFile.Api.Extensions;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Phones
{
    [ExtendObjectType(Name = "Mutation")]
    public class PhoneMutations
    {
        [UseTenantFileContext]
        public async Task<CreatePhonePayload> CreatePhone(
         CreatePhoneInput input,
         [ScopedService] TenantFileContext context,
         [Service] ITopicEventSender eventSender)
        {
            var phone = new Phone { PhoneNumber = input.PhoneNumber };
            context.Phones.Add(phone);
            context.SaveChanges();
            await eventSender.SendAsync(nameof(PhoneSubscriptions.OnNewPhoneReceived), phone.Id);
            return new CreatePhonePayload(phone);
        }
    }
}
