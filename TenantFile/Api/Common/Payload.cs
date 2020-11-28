using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantFile.Api.Common
{
    //public class Payload<T> : Payload
    //{
    //    public Payload(T t) => TPayload = t;

    //    public T? TPayload { get; init; }
    //}
    
    public abstract class Payload
    {
        protected Payload(IReadOnlyList<UserError>? errors = null) => Errors = errors;

        public IReadOnlyList<UserError>? Errors { get; }
    }
}
