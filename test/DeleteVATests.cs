using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace SignatureGenerator{

[TestFixture]
public class DeleteVATest
{
    [Test]
    public async Task DeleteVA_Test()
    {
        TestingConstant config = new TestingConstant();
        string clientId = config.ClientId;
        string privateKey = config.PrivateKey;
        string timestamp = DateTimeOffset.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
        string clientSecret = config.ClientSecret;
        string channelId = "123456";
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

        // Inquiry VA request
        ApiEndpoints apiEndpoints = new ApiEndpoints();
        VAService vaService = new VAService(apiEndpoints, clientSecret, clientId, channelId, isProduction);
        RequestBodyGenerator requestBodyGenerator = new RequestBodyGenerator(clientId);
        
        // Add the value from regist
        string VA_NO = "9912304000009286";
        string trxID = "trxId47288471";
        string Amount = "230000.00";
        string txidVa = "TNICEVA02302202409181623366653";


        string deleteRequestBody = requestBodyGenerator.GenerateDeleteVARequest(
            virtualAccountNo: VA_NO,
            trxId: trxID,
            tXidVA: txidVa,
            totalAmount: Amount
        );

        // Proses Delete VA (DELETE request dengan signature)
         string externalId = SignatureGeneratorUtils.GenerateRandomNumberString(6);
        string deleteResponse = await vaService.SendDeleteRequest(apiEndpoints.DeleteVA, accessToken, timestamp, deleteRequestBody, externalId + "03");
        Console.WriteLine("Delete VA Response: " + deleteResponse);

    }
}

}