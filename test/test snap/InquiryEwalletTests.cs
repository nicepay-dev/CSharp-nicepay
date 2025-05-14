// using NUnit.Framework;
// using Newtonsoft.Json.Linq;

// namespace SignatureGenerator{

// [TestFixture]
// public class InquiryEwalletTests
// {
//     [Test]
//     public async Task InquiryEwallet_Test()
//     {

//         TestingConstantService config = new TestingConstantService();
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

//         // Create VA request
//         ApiEndpoints apiEndpoints = new ApiEndpoints();
//         APIService ewalletService = new APIService(apiEndpoints, clientSecret, clientId, channelId,isProduction, isCloudServer);
//         SnapEwalletServices snapEwalletServices = new SnapEwalletServices(clientId);
        
//         string value = "500.00";
//         decimal totalValue = decimal.Parse(value);

//         string createRequestBody = snapEwalletServices.GenerateStatusEwalletRequest(
//        merchantId: "NORMALTEST",
//        subMerchantId: "23489182303312",
//        originalPartnerReferenceNo: "RefnoTrxSHP3823768",
//        originalReferenceNo: "NORMALTEST05202502241700546825",
//        serviceCode: "54",
//        transactionDate: DateTime.Parse(timestamp),
//        externalStoreId: "239840198240795109",
//        amountValue: value,
//        currency: "IDR"
//         );
//         Console.WriteLine("Inquiry Ewallet Request: " + createRequestBody);
//         // Send POST request to ewallet payment
//         string externalId = SignatureGeneratorUtils.GenerateRandomNumberString(6);
//         string createResponse = await ewalletService.SendPostRequest(apiEndpoints.StatusEwallet, accessToken, timestamp, createRequestBody, externalId);

//         Console.WriteLine("Inquiry Ewallet Response: " + createResponse);
//     }
// }

// }