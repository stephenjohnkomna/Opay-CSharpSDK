using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
namespace Opay_CSharpSDK.lib.core.Interfaces
{
    public interface IInquiry
    {
        Task<JObject> balanceForAllAccount();
        Task<JObject> validateMerchant(SortedDictionary<String, Object> param);
        Task<JObject> validateUser(SortedDictionary<String, Object> param);
        Task<JObject> verifyAccountAndReturnAllocatedAccountName(SortedDictionary<String, Object> param);
    }
}