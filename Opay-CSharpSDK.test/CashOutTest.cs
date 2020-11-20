using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Opay_CSharpSDK.lib.core;
using Opay_CSharpSDK.lib.core.Infrastructure;
using Opay_CSharpSDK.lib.core.Interfaces;
using Opay_CSharpSDK.lib.core.modules.Infrastructure;
using Opay_CSharpSDK.lib.core.Modules;

namespace Opay_CSharpSDK.test
{
    [TestFixture]
    public class CashOutTests
    {

        private ICashout cashout;
        private IConnectionClient connectionClient;
        private static JObject transactionCheckStatusInput;
        private readonly String BASEURL = "http://sandbox.cashierapi.operapay.com/api/v3";
        private readonly String MERCHANTID = "256620072116000";
        private readonly String PUBLICKEY = "OPAYPUB15953464969740.9412274406196679";
        private readonly String PRIVATEKEY = "OPAYPRV15953464969740.6928713062784362";


        [Test]
        public void Test_Initialize_Transaction_Successful_Message()
        {
            connectionClient = new ConnectionClient(BASEURL,
                    Util.getHeader(PUBLICKEY, MERCHANTID));
            cashout = new Cashout(connectionClient);

            SortedDictionary<String, Object> param = new SortedDictionary<String, Object>();
            param.Add("reference", Util.generateTransactionRefrenceNo());
            param.Add("mchShortName", "Jerry's shop");
            param.Add("productName", "Apple AirPods Pro");
            param.Add("productDesc", "The best wireless earphone in history");
            param.Add("userPhone", "+2349876543210");
            param.Add("userRequestIp", "123.123.123.123");
            param.Add("amount", "100");
            param.Add("currency", "NGN");
            param.Add("payMethods", new String[] { "account", "qrcode", "bankCard", "bankAccount" });
            param.Add("payTypes", new String[] { "BalancePayment", "BonusPayment", "OWealth" });
            param.Add("callbackUrl", "https://you.domain.com/callbackUrl");
            param.Add("returnUrl", "https://you.domain.com/returnUrl");
            param.Add("expireAt", "10");

            JObject response = cashout.initializeTransaction(param).Result;
            transactionCheckStatusInput = (JObject)response.GetValue("data");
            Assert.AreEqual("SUCCESSFUL", response.GetValue("message").ToString());
        }


        [Test]
        public void Test_Transaction_Check_Status_Successful()
        {

            // Sorted in Alphabetic Order
            SortedDictionary<String, Object> param = new SortedDictionary<String, Object>();
            param.Add("orderNo", transactionCheckStatusInput.GetValue("orderNo").ToString());
            param.Add("reference", transactionCheckStatusInput.GetValue("reference").ToString());

            String paramString = Util.mapToJsonString(param);
            String signature = Util.calculateHMAC(paramString, PRIVATEKEY);

            connectionClient = new ConnectionClient(BASEURL,
                    Util.getHeader(signature, MERCHANTID));
            cashout = new Cashout(connectionClient);

            JObject response = cashout.transactionStatus(param).Result;
            String message = response.GetValue("message").ToString();
            Assert.AreEqual(message, "SUCCESSFUL");
        }


        [Test]
        public void Test_Transaction_Close_Successful()
        {

            // Sorted in Alphabetic Order
            SortedDictionary<String, Object> param = new SortedDictionary<String, Object>();
            param.Add("orderNo", transactionCheckStatusInput.GetValue("orderNo").ToString());
            param.Add("reference", transactionCheckStatusInput.GetValue("reference").ToString());

            String paramString = Util.mapToJsonString(param);
            String signature = Util.calculateHMAC(paramString, PRIVATEKEY);

            connectionClient = new ConnectionClient(BASEURL,
                    Util.getHeader(signature, MERCHANTID));
            cashout = new Cashout(connectionClient);

            JObject response = cashout.closeTransaction(param).Result;
            String message = response.GetValue("message").ToString();
            Assert.AreEqual(message, "SUCCESSFUL");
        }
    }
}