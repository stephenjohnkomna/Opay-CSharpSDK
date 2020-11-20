# Opay-CSharpSDK
A C# API SDK that facilitates quick and easy development and integration of Java-based applications with the Opay Payment API.
OpayJava-SDK facilitates quick consummation of Opay Payment API and implements diverse Streams of helper methods to enable quick prototyping and testing. 

## Links
- Project: https://github.com/stephenjohnkomna/Opay-CSharpSDK  


## Getting started
### Dependencies:
- 

### Opay-CSharpSDK  installation:
- Install from the Package Manager Console using: 

### Overview
The Connection to Opay server has been abstracted, all that is required is to specify some variables when creating the connection object, then you supply the instance
of the Connection to the Constructor of the Module when instantiating them.
Please, refer to the examples for clarity.
You can also refer to the Unit Test in the code base for an elaborate implementation of all the endpoints on each modules.

Please consult the OPAY PAYMEMT API Documentation for details on setting up an account, so as to get the required keys for this integration.

### Endpoint Modules
- Cashout : This Modules consist of endpoint to do the following;
  1.Initialize a Transaction 
  2.Check a Transaction Status 
  3.Close a Transaction

- Inquiry : This Modules consist of endpoint to do the Following;
  1.Validate Opay Merchant 
  2.Validate Opay User 
  3.Validate Bank Account Number
  4.Query Balance (requests for the balances of all your OPay accounts)

- Transfer: This Modules consist of endpoint to do the Following;
  1.Transfer to Bank
  2.Transfer to Wallet
  3.Check the Status of Transfer to Bank
  4.Check the Status of Transfer to Wallet
  5.Get All Supporting Banks (fetches a list of Banks that OPay currently supports for interbank transfers)
  6.Get All Supporting Countries (fetches a list of transfer countries that OPay currently supports for bank transfers)


## Examples

### To Initialize an OPAY Transaction (Cashout Module)
```java
     // Setup the Connection Object and the Module instance
     ConnectionClient connectionClient = new ConnectionClient(BASEURL,Util.getHeader(PUBLICKEY,MERCHANTID));
     Cashout cashout = new Cashout(connectionClient);

     // Construct the Request Payload
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

           // Make the Call and get a response
            JObject response = cashout.initializeTransaction(param).Result;
```

### To Check a OPAY Transaction Status (Cashout Module)
```java
 // Setup the Connection Object and the Module instance
     ConnectionClient connectionClient = new ConnectionClient(BASEURL,Util.getHeader(PUBLICKEY,MERCHANTID));
     Cashout cashout = new Cashout(connectionClient);
	 
// Sorted in Alphabetic Order
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
```

### Signature Creation
```java
// Remember that, the Parameters keys should be arranged in Alphabetic order more reason SortedDictionary is used,  signed with the Secret Key(PRIVATEKEY) and then // hash in HMAC 512
       SortedDictionary<String, Object> param = new SortedDictionary<String, Object>();
            param.Add("orderNo", transactionCheckStatusInput.GetValue("orderNo").ToString());
            param.Add("reference", transactionCheckStatusInput.GetValue("reference").ToString());

        String paramString = Util.mapToJsonString(param);
        String signature = Util.calculateHMAC(paramString,PRIVATEKEY);
```



####  OpayJava-Sdk utilizes a background event loop and your Java application won't be able to exit until you manually shutdown all the threads by invoking:
**Remember to always shut down the API connection once you are done making requests**
```
```

### Unit Test
```NUnit
 You can folow the Unit test for sample implementation of all the endpoints.
```

## NOTE
```
  Ensure to keep your Secret key Safe. Please do not commit your secret with your code base.
```

## Utilities at a glance
### ConnectionClient [Class]:
#### methods:
- makePostRequest returns [JObject]
- shutDown [void]

### Cashout [Class]:
#### methods:
- initializeTransaction returns [JObject]
- transactionStatus returns [JObject]
- closeTransaction returns [JObject]

### Transaction [Class]:
#### methods:
- transferToBank returns [JObject]
- transferToWallet returns [JObject]
- checkBankTransferStatus returns [JObject]
- checkWalletTransferStatus returns [JObject]
- allSupportingBanks returns [JObject]
- allSupportingCountries returns [JObject]

### Inquiry [Class]:
#### methods:
- balanceForAllAccount returns [JObject]
- validateMerchant returns [JObject]
- verifyAccountAndReturnAllocatedAccountName returns [JObject]
- validateUser returns [JObject]

### Util [Class]:
#### methods:
- mapToJsonString returns [String]
- generateTransactionRefrenceNo returns [String]
- calculateHMAC returns [String]
- getHeader returns [Dictionary]


### Endpoint {Class}:
#### methods:
- A list of all the endpoints on the Opay Payment API


License
-------

The MIT License (MIT)

Copyright (c) 2020 John Stephen Komna

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
