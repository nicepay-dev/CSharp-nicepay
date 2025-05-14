// using NUnit.Framework;
// using Newtonsoft.Json.Linq;
// using Newtonsoft.Json;
// using InquiryQris;

// namespace SignatureGenerator{

// [TestFixture]
// public class InquiryQrisTests
// {
//     [Test]
//     public async Task InquiryQris_Test()
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
//         Console.WriteLine("Inquiry Qris Akses token: " + accessToken);

//         // Create VA request
//         ApiEndpoints apiEndpoints = new ApiEndpoints();
//         APIService qrisService = new APIService(apiEndpoints, clientSecret, clientId, channelId,isProduction, isCloudServer);
//         SnapQrisServices snapQrisServices = new SnapQrisServices(clientId);
    

//        string createRequestBody = snapQrisServices.GenerateInquiryQris(
//        originalReferenceNo: "TNICEQR08108202502271702432504",
//        originalPartnerReferenceNo: "ncpy26557769",
//        merchantId: "TNICEQR081",
//        externalStoreId: "NicepayStoreID1",
//        serviceCode: "47"
//         );
       
//        Console.WriteLine("Inquiry Qris Request: " + createRequestBody);
//         // Send POST request to ewallet payment
//         string externalId = SignatureGeneratorUtils.GenerateRandomNumberString(6);
//         string createResponse = await qrisService.SendPostRequest(apiEndpoints.StatusQris, accessToken, timestamp, createRequestBody, externalId);

//         Console.WriteLine("Inquiry Qris Response: " + createResponse);
//     }
// }

// }