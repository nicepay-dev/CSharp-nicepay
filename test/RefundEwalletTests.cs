using NUnit.Framework;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SignatureGenerator{

[TestFixture]
public class RefundEwalletTests
{
    [Test]  
    public async Task RefundEwallet_Test()
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

        string createRequestBody = snapEwalletServices.GenerateRefundEwalletRequest(
       merchantId: "NORMALTEST",
       subMerchantId: "23489182303312",
       originalPartnerReferenceNo: "RefnoTrxSHP3823768",
       originalReferenceNo: "NORMALTEST05202411280837115452",
       partnerRefundNo: "CANCELTESTING20241128",
       externalStoreId: "239840198240795109",
       refundAmountValue: value,
       currency: "IDR",
       reason : "cancel the transaction",
       refundType : "1"
        );
        // Send POST request to ewallet payment
        string externalId = SignatureGeneratorUtils.GenerateRandomNumberString(6);
        string createResponse = await ewalletService.SendPostRequest(apiEndpoints.RefundEwallet, accessToken, timestamp, createRequestBody, externalId);

        Console.WriteLine("Inquiry Ewallet Response: " + createResponse);
    }
}

}