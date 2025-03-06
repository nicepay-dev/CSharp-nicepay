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

        // Inquiry VA request
        ApiEndpoints apiEndpoints = new ApiEndpoints();
        APIService vaService = new APIService(apiEndpoints, clientSecret, clientId, channelId, isProduction,isCloudServer);
        SnapVaServices requestBodyGenerator = new SnapVaServices(clientId);
        
        // Add the value from regist
        string VA_NO = "9912304000008854";
        string trxID = "trxId40949767";
        string Amount = "230000.00"; // Mengganti nama dari Amount menjadi totalAmountValue
        string currency = "IDR"; // Menentukan mata uang
        string txidVa = "NORMALTEST02202502241029567253";
        string cancelMessage = "Request to delete virtual account"; // Menentukan pesan pembatalan



        string deleteRequestBody = requestBodyGenerator.GenerateDeleteVARequest(
        virtualAccountNo: VA_NO,
        trxId: trxID,
        value: Amount, // Menambahkan parameter totalAmountValue
        currency: currency, // Menambahkan parameter currency
        tXidVA: txidVa,
        customerNo : "",
        cancelMessage: cancelMessage // Menambahkan parameter cancelMessage
        );

        Console.WriteLine("Delete VA Request: " + deleteRequestBody);
        // Proses Delete VA (DELETE request dengan signature)
         string externalId = SignatureGeneratorUtils.GenerateRandomNumberString(6);
        string deleteResponse = await vaService.SendDeleteRequest(apiEndpoints.DeleteVA, accessToken, timestamp, deleteRequestBody, externalId + "03");
        Console.WriteLine("Delete VA Response: " + deleteResponse);

    }
}

}