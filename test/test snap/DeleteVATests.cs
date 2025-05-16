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

        // Inquiry VA request
        ApiEndpoints apiEndpoints = new ApiEndpoints();
        APIService vaService = new APIService(apiEndpoints, clientSecret, clientId, channelId, isProduction,isCloudServer);
        
        // Add the value from regist
        string VA_NO = "9912304000008854";
        string trxID = "trxId40949767";
        string Amount = "230000.00"; // Mengganti nama dari Amount menjadi totalAmountValue
        string currency = "IDR"; // Menentukan mata uang
        string txidVa = "NORMALTEST02202502241029567253";
        string cancelMessage = "Request to delete virtual account"; // Menentukan pesan pembatalan



        var bodyBuilder = new CancelBuilder()
        .SetCancelVASnap(
        partnerServiceId :"",
        virtualAccountNo: VA_NO,
        trxId: trxID,
        amountValue: Amount, // Menambahkan parameter totalAmountValue
        currency: currency, // Menambahkan parameter currency
        tXidVA: txidVa,
        customerNo : "",
        cancelMessage: cancelMessage // Menambahkan parameter cancelMessage
        ).Build();

        string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(bodyBuilder);
        Console.WriteLine("Delete VA Request: " + jsonPayload);
        // Proses Delete VA (DELETE request dengan signature)
         string externalId = SignatureGeneratorUtils.GenerateRandomNumberString(6);
        string deleteResponse = await vaService.SendDeleteRequest(apiEndpoints.DeleteVA, accessToken, timestamp, bodyBuilder, externalId + "03");
        Console.WriteLine("Delete VA Response: " + deleteResponse);

    }
}

}