using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace SignatureGenerator{

[TestFixture]
public class CreateVATests
{
    [Test]
    public async Task CreateVA_Test()
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
        Console.WriteLine("Create VA Akses token: " + accessTokenResponse);

        // Create VA request
        ApiEndpoints apiEndpoints = new ApiEndpoints();
        APIService vaService = new APIService(apiEndpoints, clientSecret, clientId, channelId,isProduction);
        SnapVaServices requestBodyGenerator = new SnapVaServices(clientId);
        string createRequestBody = requestBodyGenerator.GenerateCreateVARequest(
            customerNo: "",
            virtualAccountName: "John Test",
            trxId: "trxId" + random,
            totalAmountValue: "230000.00",
            currency: "IDR",
            bankCd: "CENA",
            goodsNm: "Test",
            dbProcessUrl: "https://nicepay.co.id/"
        );
       
        // Send POST request to create VA
        string externalId = SignatureGeneratorUtils.GenerateRandomNumberString(6);
        string createResponse = await vaService.SendPostRequest(apiEndpoints.CreateVA, accessToken, timestamp, createRequestBody, externalId);

        dynamic createVAResponse = JObject.Parse(createResponse);
        // string va_num = createVAResponse.virtualAccountData["virtualAccountNo"];
        // string amount = createVAResponse.virtualAccountData["totalAmount"]["value"].ToString();
        // string txidVa = createVAResponse.virtualAccountData["additionalInfo"]["tXidVA"].ToString();
        // string trxID = createVAResponse.virtualAccountData["trxId"].ToString();

        // TestDataStore.VaNum = va_num;
        // TestDataStore.TrxId = trxID;
        // TestDataStore.Amount = amount;
        // TestDataStore.TXidVA = txidVa;
        // //Assert.NotNull(createResponse);
        // Console.WriteLine($"VaNum: {TestDataStore.VaNum}");
        // Console.WriteLine($"TrxId: {TestDataStore.TrxId}");
        // Console.WriteLine($"Amount: {TestDataStore.Amount}");
        // Console.WriteLine($"TXidVA: {TestDataStore.TXidVA}");
        Console.WriteLine("Create VA Response: " + createResponse);
    }
}

}