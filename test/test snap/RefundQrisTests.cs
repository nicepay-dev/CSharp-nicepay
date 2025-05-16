// using NUnit.Framework;
// using Newtonsoft.Json.Linq;
// using Newtonsoft.Json;
// using RefundQris;

// namespace SignatureGenerator{

// [TestFixture]
// public class RefundQrisTests
// {
//     [Test]  
//     public async Task RefundQris_Test()
//     {

//         TestingConstant config = new TestingConstant();
//         string clientId = config.ClientId;
//         string privateKey = config.PrivateKey;
//         string timestamp = DateTimeOffset.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
//         string clientSecret = config.ClientSecret;
//         string channelId = "123456";
//         string random = SignatureGeneratorUtils.GenerateRandomNumberString(8);
//         bool isProduction = false;
//         bool isCloudServer = true;

//           if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
//             {
//                 Console.WriteLine("clientId or clientSecret cannot be empty.");
//                 Assert.Fail("clientId or clientSecret cannot be empty.");
//                 return;
//             }
//         // Generate signature
//         string signature = SignatureGeneratorUtils.GenerateSignature(privateKey,clientId,timestamp);

//         // Get access token
//          var tokenRequester = new AccessTokenRequester();
//         string accessTokenResponse = await tokenRequester.GetAccessTokenAsync(clientId, signature, timestamp, isProduction, isCloudServer);
//         dynamic accessTokenObject = JObject.Parse(accessTokenResponse);
//         string accessToken = accessTokenObject.accessToken;
//         Console.WriteLine("Inquiry Ewallet Akses token: " + accessToken);

//         // Create QRIS request
//         ApiEndpoints apiEndpoints = new ApiEndpoints();
//         APIService qrisService = new APIService(apiEndpoints, clientSecret, clientId, channelId,isProduction, isCloudServer);
//         SnapQrisServices snapQrisServices = new SnapQrisServices(clientId);
        
//         string value = "500.00";
//         decimal totalValue = decimal.Parse(value);

//         string createRequestBody = snapQrisServices.GenerateRefundQris(
//        originalReferenceNo: "TNICEQR08108202411300924473784",
//        originalPartnerReferenceNo: "ncpy40942494",
//        merchantId: "TNICEQR081",
//        partnerRefundNo: "ncpyRefund" + random ,
//        externalStoreId: "NicepayStoreID1",
//        value: value,
//        cancelType: "1",
//        currency: "IDR",
//        reason : "cancel the transaction from plugin C SHarp"
//         );

//         // Send POST request to ewallet payment
//         string externalId = SignatureGeneratorUtils.GenerateRandomNumberString(6);
//         string createResponse = await qrisService.SendPostRequest(apiEndpoints.RefundQris, accessToken, timestamp, createRequestBody, externalId);

//         Console.WriteLine("Refund Qris Response: " + createResponse);
//     }
// }

// }