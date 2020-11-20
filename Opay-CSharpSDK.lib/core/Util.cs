using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace Opay_CSharpSDK.lib.core
{
    public class Util
    {

        public static String mapToJsonString(SortedDictionary<String, Object> parameters)
        {
            String json = JsonConvert.SerializeObject(parameters);
            return json;
        }

        public static String generateTransactionRefrenceNo()
        {
            String value = "test_" + Guid.NewGuid().ToString().Replace("-", "");
            return value;
        }



        public static string calculateHMAC(string data, string key)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            using (var shaAlgorithm = new HMACSHA512(keyBytes))
            {
                var signatureBytes = Encoding.UTF8.GetBytes(data);
                var signatureHashBytes = shaAlgorithm.ComputeHash(signatureBytes);
                return string.Concat(Array.ConvertAll(signatureHashBytes, b => b.ToString("X2")))
                    .ToLower();
            }
        }

        public static Dictionary<String, String> getHeader(String authorization, String merchantId)
        {
            Dictionary<String, String> hearders = new Dictionary<String, String>();
            hearders.Add("Accept", "Application/json");
            hearders.Add("Content-Type", "Application/json");
            hearders.Add("Authorization", "Bearer " + authorization);
            hearders.Add("MerchantId", merchantId);
            return hearders;
        }

    }
}