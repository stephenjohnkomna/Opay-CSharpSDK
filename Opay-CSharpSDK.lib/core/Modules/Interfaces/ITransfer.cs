using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
namespace Opay_CSharpSDK.lib.core.Interfaces
{
    public interface ITransfer
    {
        Task<JObject> transferToBank(SortedDictionary<String, Object> param);
        Task<JObject> transferToWallet(SortedDictionary<String, Object> param);
        Task<JObject> checkBankTransferStatus(SortedDictionary<String, Object> param);
        Task<JObject> checkWalletTransferStatus(SortedDictionary<String, Object> param);
        Task<JObject> allSupportingBanks(SortedDictionary<String, Object> param);
        Task<JObject> allSupportingCountries();
    }
}