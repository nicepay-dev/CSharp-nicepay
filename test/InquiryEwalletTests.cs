using NUnit.Framework;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using InquiryEwallet;

namespace SignatureGenerator{

[TestFixture]
public class InquiryEwalletTests
{
    [Test]
    public async Task InquiryEwallet_Test()
    {

        TestingConstant config = new TestingConstant();
        string clientId = config.ClientId;
        string privateKey = config.PrivateKey;
        string timestamp = DateTimeOffset.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
        string clientSecret = config.ClientSecret;
        string channelId = "123456";
        string random = SignatureGeneratorUtils.GenerateRandomNumberString(8);
        bool isProduction = false;

          if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
            {
                Console.WriteLine("clientId or clientSecret cannot be empty.");
                Assert.Fail("clientId or clientSecret cannot be empty.");
                return;
            }
        // Generate signature
        var signatureGenerator = new SignatureGeneratorUtils();
        string stringToSign = signatureGenerator.GenerateStringToSign(clientId, timestamp);
        string signature = SignatureGeneratorUtils.GenerateSignature(privateKey, stringToSign);

        // Get access token
         var tokenRequester = new AccessTokenRequester();
        string accessTokenResponse = await tokenRequester.GetAccessTokenAsync(clientId, signature, timestamp, isProduction);
        dynamic accessTokenObject = JObject.Parse(accessTokenResponse);
        string accessToken = accessTokenObject.accessToken;
        Console.WriteLine("Inquiry Ewallet Akses token: " + accessToken);

        // Create VA request
        ApiEndpoints apiEndpoints = new ApiEndpoints();
        APIService ewalletService = new APIService(apiEndpoints, clientSecret, clientId, channelId,isProduction);
        SnapEwalletServices snapEwalletServices = new SnapEwalletServices(clientId);
        
        string value = "500.00";
        decimal totalValue = decimal.Parse(value);

        string createRequestBody = snapEwalletServices.GenerateStatusEwalletRequest(
       merchantId: "NORMALTEST",
       subMerchantId: "23489182303312",
       originalPartnerReferenceNo: "RefnoTrxSHP3823768",
       originalReferenceNo: "NORMALTEST05202411280837115452",
       serviceCode: "54",
       transactionDate: DateTime.Parse(timestamp),
       externalStoreId: "239840198240795109",
       amountValue: value,
       currency: "IDR"
        );
       
        // Send POST request to ewallet payment
        string externalId = SignatureGeneratorUtils.GenerateRandomNumberString(6);
        string createResponse = await ewalletService.SendPostRequest(apiEndpoints.StatusEwallet, accessToken, timestamp, createRequestBody, externalId);

        Console.WriteLine("Inquiry Ewallet Response: " + createResponse);
    }
}

}