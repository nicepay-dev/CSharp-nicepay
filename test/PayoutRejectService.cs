using NUnit.Framework;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using CreatePayout;

namespace SignatureGenerator{

[TestFixture]
public class PayoutRejectService
{
    [Test]
    public async Task rejectPayout_Test()
    {

        TestingConstant config = new TestingConstant();
        string clientId = config.ClientId;
        string privateKey = config.PrivateKey;
        string timestamp = DateTimeOffset.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
        string clientSecret = config.ClientSecret;
        string channelId = "123456";
        string random = SignatureGeneratorUtils.GenerateRandomNumberString(8);
        bool isProduction = false;
        bool isCloudServer = true;

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
        string accessTokenResponse = await tokenRequester.GetAccessTokenAsync(clientId, signature, timestamp, isProduction, isCloudServer);
        dynamic accessTokenObject = JObject.Parse(accessTokenResponse);
        string accessToken = accessTokenObject.accessToken;
        Console.WriteLine("Reject Payout Akses token: " + accessToken);

        // Create VA request
        ApiEndpoints apiEndpoints = new ApiEndpoints();
        APIService payoutService = new APIService(apiEndpoints, clientSecret, clientId, channelId,isProduction, isCloudServer);
        SnapPayoutServices snapPayoutServices = new SnapPayoutServices(clientId);
        

        // Jumlah item dan distribusi nilai `goodsAmt` ke setiap item
        
        string createRequestBody = snapPayoutServices.RejectPayoutRequest(
        originalPartnerReferenceNo :"TNICEPO07107202412021039322043", 
        originalReferenceNo : "TSPYT90709248",
        merchantId:"TNICEPO071"
        );
       
        // Send POST request to ewallet payment
        string externalId = SignatureGeneratorUtils.GenerateRandomNumberString(6);
        string createResponse = await payoutService.SendPostRequest(apiEndpoints.RejectPayout, accessToken, timestamp, createRequestBody, externalId);

        Console.WriteLine("Reject Payout Response: " + createResponse);
    }
}

}