using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantFile.Api.Common
{
      public abstract class Payload
    {
        protected Payload(IReadOnlyList<UserError>? errors = null) => Errors = errors;

        public IReadOnlyList<UserError>? Errors { get; }
    }
}
