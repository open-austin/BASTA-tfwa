using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantFile.Api.Common
{
    public class TPayload<T> : Payload
    {
        protected TPayload(T t) => Payload = t;
        protected TPayload(IReadOnlyList<UserError> errors) : base(errors) { }
        public T? Payload { get; init; }
    }
}
