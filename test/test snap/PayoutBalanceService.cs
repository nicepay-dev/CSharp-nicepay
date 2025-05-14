using NUnit.Framework;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SignatureGenerator{

[TestFixture]
public class PayoutBalanceService
{
    [Test]
    public async Task balancePayout_Test()
    {

        TestingConstantPayout config = new TestingConstantPayout();
        string clientId = config.ClientId;
        string privateKey = config.PrivateKey;
        string timestamp = DateTimeOffset.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
        string clientSecret = config.ClientSecret;
        string channelId = "123456";
        bool isProduction = false;
        bool isCloudServer = false;

          if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
            {
                Console.WriteLine("clientId or clientSecret cannot be empty.");
                Assert.Fail("clientId or clientSecret cannot be empty.");
                return;
            }
        // Generate signature
        string signature = SignatureGeneratorUtils.GenerateSignature(privateKey,clientId,timestamp);

        // Get access token
         var tokenRequester = new AccessTokenRequester();
        string accessTokenResponse = await tokenRequester.GetAccessTokenAsync(clientId, signature, timestamp, isProduction, isCloudServer);
        dynamic accessTokenObject = JObject.Parse(accessTokenResponse);
        string accessToken = accessTokenObject.accessToken;
        Console.WriteLine("Balance Payout Akses token: " + accessToken);

        // Create VA request
        ApiEndpoints apiEndpoints = new ApiEndpoints();
        APIService payoutService = new APIService(apiEndpoints, clientSecret, clientId, channelId,isProduction, isCloudServer);
        

        
        var bodyBuilder = new PayoutBuilder()
        .SetBalancePayoutSnap(
        accountNo :clientId, 
        msId : ""
        ).Build();
       
        // Send POST request to ewallet payment
         string jsonPayload = JsonConvert.SerializeObject(bodyBuilder);
         Console.WriteLine("Inquiry Balance Request: " + jsonPayload);
        string externalId = SignatureGeneratorUtils.GenerateRandomNumberString(6);
        string createResponse = await payoutService.SendPostRequest(apiEndpoints.BalancePayout, accessToken, timestamp, bodyBuilder, externalId);

        Console.WriteLine("Balance Payout Response: " + createResponse);
    }
}

}