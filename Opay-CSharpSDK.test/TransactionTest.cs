using System;
using System.Collections.Generic;
using System.Transactions;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Opay_CSharpSDK.lib.core;
using Opay_CSharpSDK.lib.core.Infrastructure;
using Opay_CSharpSDK.lib.core.Interfaces;
using Opay_CSharpSDK.lib.core.modules.Infrastructure;
using Opay_CSharpSDK.lib.core.Modules;

namespace Opay_CSharpSDK.test
{
    [TestFixture]
    public class TransactionTest
    {
        private ITransfer transaction;
        private IConnectionClient connectionClient;

        private static JObject bankTransferStatusInput;
        private static JObject walletTransferUserStatusInput;
        private static JObject walletTransferMerchantStatusInput;

        private readonly String BASEURL = "http://sandbox.cashierapi.operapay.com/api/v3";
        private readonly String MERCHANTID = "256620112018031";
        private readonly String PUBLICKEY = "OPAYPUB16058777635980.9961229244591103";
        private readonly String PRIVATEKEY = "OPAYPRV16058777635980.3804652128291669";

        [Test]
        public void Test_Transfer_To_Bank_Successful()
        {
            SortedDictionary<String, Object> receiver = new SortedDictionary<String, Object>();
            receiver.Add("bankAccountNumber", "22222222222222");
            receiver.Add("bankCode", "058");
            receiver.Add("name", "test_20191123132233");

            SortedDictionary<String, Object> param = new SortedDictionary<String, Object>();
            param.Add("amount", "100");
            param.Add("country", "NG");
            param.Add("currency", "NGN");
            param.Add("reason", "transfer reason message");
            param.Add("receiver", receiver);
            param.Add("reference", Util.generateTransactionRefrenceNo());

            String paramString = Util.mapToJsonString(param);
            String signature = Util.calculateHMAC(paramString, PRIVATEKEY);

            connectionClient = new ConnectionClient(BASEURL, Util.getHeader(signature, MERCHANTID));
            transaction = new Transfer(connectionClient);

            JObject response = transaction.transferToBank(param).Result;
            bankTransferStatusInput = (JObject)response.GetValue("data");
            String message = response.GetValue("message").ToString();
            Assert.AreEqual(message, "SUCCESSFUL");
        }


        [Test]
        public void Test_Bank_Transfer_Status_Successful()
        {
            SortedDictionary<String, Object> param = new SortedDictionary<String, Object>();
            param.Add("orderNo",bankTransferStatusInput.GetValue("orderNo").ToString());
            param.Add("reference",bankTransferStatusInput.GetValue("reference").ToString());

            String paramString = Util.mapToJsonString(param);
            String signature = Util.calculateHMAC(paramString, PRIVATEKEY);

            connectionClient = new ConnectionClient(BASEURL,
                    Util.getHeader(signature, MERCHANTID));
            transaction = new Transfer(connectionClient);

            JObject response = transaction.checkBankTransferStatus(param).Result;
            Assert.AreEqual("SUCCESSFUL", response.GetValue("message"));
        }




        [Test]
        public void Test_Transfer_To_User_Wallet_Successful()
        {
            SortedDictionary<String, Object> receiver = new SortedDictionary<String, Object>();
            receiver.Add("name", "Adny Lee");
            receiver.Add("phoneNumber", "+2348131393827");
            receiver.Add("type", "USER");


            SortedDictionary<String, Object> param = new SortedDictionary<String, Object>();
            param.Add("amount", "100");
            param.Add("country", "NG");
            param.Add("currency", "NGN");
            param.Add("reason", "transfer reason message");
            param.Add("receiver", receiver);
            param.Add("reference", Util.generateTransactionRefrenceNo());

            String paramString = Util.mapToJsonString(param);
            String signature = Util.calculateHMAC(paramString, PRIVATEKEY);

            connectionClient = new ConnectionClient(BASEURL, Util.getHeader(signature, MERCHANTID));
            transaction = new Transfer(connectionClient);

            JObject response = transaction.transferToWallet(param).Result;
            walletTransferUserStatusInput =(JObject) response.GetValue("data");
            Assert.AreEqual("SUCCESSFUL", response.GetValue("message").ToString());
        }

        [Test]
        public void Test_Query_Transfer_To_User_Wallet_Status_Successful()
        {
            // Sorted in Alphabetic Order
            SortedDictionary<String, Object> param = new SortedDictionary<String, Object>();
            param.Add("orderNo",walletTransferUserStatusInput.GetValue("orderNo"));
            param.Add("reference",walletTransferUserStatusInput.GetValue("reference"));

            String paramString = Util.mapToJsonString(param);
            String signature = Util.calculateHMAC(paramString, PRIVATEKEY);

            connectionClient = new ConnectionClient(BASEURL,
                    Util.getHeader(signature, MERCHANTID));
            transaction = new Transfer(connectionClient);

            JObject response = transaction.checkWalletTransferStatus(param).Result;
            Assert.AreEqual("SUCCESSFUL", response.GetValue("message").ToString());
        }



        [Test]
        public void Test_Transfer_To_Merchant_Wallet_Successful()
        {
            SortedDictionary<String, Object> receiver = new SortedDictionary<String, Object>();
            receiver.Add("name", "Andy Lee");
            receiver.Add("type", "MERCHANT");
            receiver.Add("merchantId", "256620111818011");

            SortedDictionary<String, Object> param = new SortedDictionary<String, Object>();
            param.Add("amount", "100");
            param.Add("country", "NG");
            param.Add("currency", "NGN");
            param.Add("reason", "transfer reason message");
            param.Add("receiver", receiver);
            param.Add("reference", Util.generateTransactionRefrenceNo());

            String paramString = Util.mapToJsonString(param);
            String signature = Util.calculateHMAC(paramString, PRIVATEKEY);

            connectionClient = new ConnectionClient(BASEURL, Util.getHeader(signature, MERCHANTID));
            transaction = new Transfer(connectionClient);

            JObject response = transaction.transferToWallet(param).Result;
            walletTransferMerchantStatusInput =response;
            Assert.AreEqual("SUCCESSFUL", response.GetValue("message").ToString());
        }

        [Test]
        public void Test_Query_Transfer_To_Merchant_Wallet_Status_Successful()
        {
            SortedDictionary<String, Object> param = new SortedDictionary<String, Object>();
            param.Add("orderNo",walletTransferMerchantStatusInput.GetValue("orderNo"));
            param.Add("reference",walletTransferMerchantStatusInput.GetValue("reference"));

            String paramString = Util.mapToJsonString(param);
            String signature = Util.calculateHMAC(paramString, PRIVATEKEY);

            connectionClient = new ConnectionClient(BASEURL,
                    Util.getHeader(signature, MERCHANTID));
            transaction = new Transfer(connectionClient);

            JObject response = transaction.checkWalletTransferStatus(param).Result;
            Assert.AreEqual("SUCCESSFUL", response.GetValue("message").ToString());
        }

        [Test]
        public void Test_Get_All_Supporting_Banks()
        {
            SortedDictionary<String, Object> param = new SortedDictionary<String, Object>();
            param.Add("countryCode", "NG");

            connectionClient = new ConnectionClient(BASEURL, Util.getHeader(PUBLICKEY, MERCHANTID));
            transaction = new Transfer(connectionClient);

            JObject response = transaction.allSupportingBanks(param).Result;
            Assert.AreEqual("SUCCESSFUL", response.GetValue("message").ToString());
        }

        [Test]
        public void Test_Get_All_Supporting_Countries()
        {
            connectionClient = new ConnectionClient(BASEURL, Util.getHeader(PUBLICKEY, MERCHANTID));
            transaction = new Transfer(connectionClient);
            JObject response = transaction.allSupportingCountries().Result;
            Assert.AreEqual("SUCCESSFUL", response.GetValue("message").ToString());
        }

    }
}
