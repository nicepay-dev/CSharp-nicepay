using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace SignatureGenerator{

[TestFixture]
public class CreateVATests
{
    [Test]
    public async Task CreateVA_Test()
    {

        TestingConstantService config = new TestingConstantService();
        string clientId = config.ClientId;
        string privateKey = config.PrivateKey;
        string timestamp = config.Timestamp;
        string clientSecret = config.ClientSecret;
        string channelId = "123456";
        string random = SignatureGeneratorUtils.GenerateRandomNumberString(8);
        string externalId = SignatureGeneratorUtils.GenerateRandomNumberString(6);
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
        string accessTokenResponse = await tokenRequester.GetAccessTokenAsync(clientId, signature, timestamp, isProduction,isCloudServer);
        dynamic accessTokenObject = JObject.Parse(accessTokenResponse);
        string accessToken = accessTokenObject.accessToken;
        Console.WriteLine("Create VA Akses token: " + accessTokenResponse);

        // Create VA request
        ApiEndpoints apiEndpoints = new ApiEndpoints();
        APIService vaService = new APIService(apiEndpoints, clientSecret, clientId, channelId,isProduction, isCloudServer);
       
        
        var bodyBuilder = new VirtualAccountBuilder()
          .SetVirtualAccountSnap(
            partnerServiceId : "",
            customerNo : "",
            virtualAccountName : "Testing c# plugin",
            trxId : "trxId" + random,
            value : "20000.00",
            currency : "IDR",
            bankCd : "CENA",
            goodsNm : "Testing plugin c#",
            dbProcessUrl : "https://nicepay.co.id/"
          ).Build();


       
        // Send POST request to create VA
         string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(bodyBuilder);
         Console.WriteLine("Create VA Request: " + jsonPayload);
        string createResponse = await vaService.SendPostRequest(apiEndpoints.CreateVA, accessToken, timestamp, bodyBuilder, externalId);
        
        Console.WriteLine("Create VA Response: " + createResponse);
    }
}

}