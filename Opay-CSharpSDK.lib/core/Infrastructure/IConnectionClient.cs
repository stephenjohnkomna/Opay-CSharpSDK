using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Opay_CSharpSDK.lib.core.Infrastructure
{
    public interface IConnectionClient
    {
        Task<JObject> makePostRequestAsync(SortedDictionary<string, object> parameters, string endpoint);
        Task<JObject> makePostRequestAsync(String endpoint);
    }
}
