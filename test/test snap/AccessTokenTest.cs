using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace SignatureGenerator{

[TestFixture]
public class AccessTokenTest
{
    [Test]
    public async Task AccessToken_Test()
    {

        TestingConstant config = new TestingConstant();
        string clientId = config.ClientId;
        string privateKey = config.PrivateKey;
        string timestamp = DateTimeOffset.Now.AddMinutes(5).ToString("yyyy-MM-ddTHH:mm:sszzz");
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
        string signature = SignatureGeneratorUtils.GenerateSignature(privateKey,clientId,timestamp);
        // Get access token
         var tokenRequester = new AccessTokenRequester();
        string accessTokenResponse = await tokenRequester.GetAccessTokenAsync(clientId, signature, timestamp, isProduction,isCloudServer);
        dynamic accessTokenObject = JObject.Parse(accessTokenResponse);
        string accessToken = accessTokenObject.accessToken;
        Console.WriteLine("Generate Qris Akses token: " + accessToken);


        
                }
        }

}