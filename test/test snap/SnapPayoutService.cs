using NUnit.Framework;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SignatureGenerator{

[TestFixture]
public class SnapPayoutService
{
    [Test]
    public async Task createPayout_Test()
    {

        TestingConstantPayout config = new TestingConstantPayout();
        string clientId = config.ClientId;
        string privateKey = config.PrivateKey;
        string timestamp = DateTimeOffset.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"); 
        string clientSecret = config.ClientSecret;
        string channelId = "TNICEPO071";
        string random = SignatureGeneratorUtils.GenerateRandomNumberString(8);
        bool isProduction = false;
        bool isCloudServer = false;
        string reservedDt = DateTime.Now.ToString("yyyyMMdd");
        string reservedTm = DateTime.Now.AddMinutes(70).ToString("HHmmss");

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
        Console.WriteLine("Create Payout Akses token: " + accessToken);

        // Create VA request
        ApiEndpoints apiEndpoints = new ApiEndpoints();
        APIService payoutService = new APIService(apiEndpoints, clientSecret, clientId, channelId,isProduction, isCloudServer);
        
        string value = "10000.00";
        decimal totalValue = decimal.Parse(value);

        // Jumlah item dan distribusi nilai `goodsAmt` ke setiap item
        
        var bodyBuilder = new PayoutBuilder()
        .SetPayoutSnap(
        merchantId : clientId, 
        beneficiaryAccountNo:"08123456789", 
        beneficiaryName:"IONPAY NETWORKS", 
        beneficiaryPhone:"08123456789",
        payoutMethod:"", 
        beneficiaryBankCode:"BNIN",  
        partnerReferenceNo:"TSPYT" + random,
        beneficiaryCustomerType:"1",
        beneficiaryCustomerResidence:"1", 
        reservedTm : reservedTm,
        reservedDt : reservedDt,
        value:value,
        currency:"IDR",
        description:"This is test Request C Sharp"
        ).Build();

        string jsonPayload = JsonConvert.SerializeObject(bodyBuilder);
        Console.WriteLine("Create Payout Request: " + jsonPayload);
        // Send POST request to ewallet payment
        string externalId = SignatureGeneratorUtils.GenerateRandomNumberString(6);
        string createResponse = await payoutService.SendPostRequest(apiEndpoints.CreatePayout, accessToken, timestamp, bodyBuilder, externalId);

        Console.WriteLine("Create Payout Response: " + createResponse);
    }
}

}