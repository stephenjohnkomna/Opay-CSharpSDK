using System;
using System.Collections.Generic;
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
    public class InquiryTests
    {

        private IInquiry inquiry;
        private IConnectionClient connectionClient;
        private readonly String BASEURL = "http://sandbox.cashierapi.operapay.com/api/v3";
        private readonly String MERCHANTID = "256620112018031";
        private readonly String PUBLICKEY = "OPAYPUB16058777635980.9961229244591103";
       

        [Test]
        public void Test_Query_Balance_All_Account_Success()
        {
            connectionClient = new ConnectionClient(BASEURL, Util.getHeader(PUBLICKEY, MERCHANTID));
            inquiry = new Inquiry(connectionClient);

            JObject response = inquiry.balanceForAllAccount().Result;
            String message = response.GetValue("message").ToString();
            JObject data = (JObject)response.GetValue("data");
            Assert.AreEqual(message, "SUCCESSFUL");
        }


        [Test]
        public void Test_Validate_Merchant_Successful()
        {
            SortedDictionary<String, Object> param = new SortedDictionary<String, Object>();
            param.Add("email", "aagboola@opay-inc.com");
            connectionClient = new ConnectionClient(BASEURL, Util.getHeader(PUBLICKEY, MERCHANTID));
            inquiry = new Inquiry(connectionClient);

           
            JObject response = inquiry.validateMerchant(param).Result;
            String message = response.GetValue("message").ToString();
            Assert.AreEqual(message, "SUCCESSFUL");
        }

        [Test]
        public void Test_Validate_User_Successful()
        {
            SortedDictionary<String, Object> param = new SortedDictionary<String, Object>();
            param.Add("phoneNumber", "+2349876543210");

            connectionClient = new ConnectionClient(BASEURL, Util.getHeader(PUBLICKEY, MERCHANTID));
            inquiry = new Inquiry(connectionClient);

            JObject response = inquiry.validateUser(param).Result;
            String message = response.GetValue("message").ToString();
            Assert.AreEqual(message, "SUCCESSFUL");
        }


        [Test]
        public void Test_Verify_AccountAndReturnAllocatedAccountName_Successful()
        {
            SortedDictionary<String, Object> param = new SortedDictionary<String, Object>();
            param.Add("bankCode", "058");
            param.Add("bankAccountNo", "45345343434");
            param.Add("countryCode", "NG");

            connectionClient = new ConnectionClient(BASEURL, Util.getHeader(PUBLICKEY, MERCHANTID));
            inquiry = new Inquiry(connectionClient);

            
            JObject response = inquiry.verifyAccountAndReturnAllocatedAccountName(param).Result;
            String message = response.GetValue("message").ToString();
            Assert.AreEqual(message, "SUCCESSFUL");
        }
    }
}
