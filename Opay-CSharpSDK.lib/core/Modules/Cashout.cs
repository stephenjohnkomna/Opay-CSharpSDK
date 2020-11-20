using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Opay_CSharpSDK.lib.core.Infrastructure;
using Opay_CSharpSDK.lib.core.Interfaces;
using Opay_CSharpSDK.lib.core.modules.Infrastructure;

namespace Opay_CSharpSDK.lib.core.Modules
{
    public class Cashout : ICashout
    {
    private IConnectionClient _connectionClient;


    public Cashout(IConnectionClient connectionClient)
    {
        this._connectionClient = connectionClient;
    }

    public Task<JObject> initializeTransaction(SortedDictionary<String, Object> param) {
            return _connectionClient.makePostRequestAsync(param, Endpoint.OPAY_CHECKOUT_INITIALIZE_TRANSACTION);
    }

    public Task<JObject> transactionStatus(SortedDictionary<String, Object> param) {
            return _connectionClient.makePostRequestAsync(param, Endpoint.OPAY_CHECKOUT_TRANSACTION_STATUS);
    }

    public Task<JObject> closeTransaction(SortedDictionary<String, Object> param) {
        return _connectionClient.makePostRequestAsync(param,Endpoint.OPAY_CHECKOUT_CLOSE_TRANSACTION);
    }
    }
}