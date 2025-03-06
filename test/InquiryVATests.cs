using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace SignatureGenerator{

[TestFixture]
public class InquiryVATests
{
    [Test]
    public async Task InquiryVA_Test()
    {
        TestingConstantService config = new TestingConstantService();
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
        APIService vaService = new APIService(apiEndpoints, clientSecret, clientId, channelId,isProduction, isCloudServer);
        SnapVaServices requestBodyGenerator = new SnapVaServices(clientId);
        
        // Add the value from regist
        string VA_NO = "9912304000008854";
        string trxID = "trxId40949767";
        string Amount = "230000.00";
        string txidVa = "NORMALTEST02202502241029567253";


        string inquiryRequestBody = requestBodyGenerator.GenerateInquiryRequest(
            customerNo: "",
            virtualAccountNo: VA_NO,
            inquiryRequestId: SignatureGeneratorUtils.GenerateRandomNumberString(6),
            value: Amount,
            currency: "IDR",
            trxId: trxID,
            tXidVA: txidVa
        );

        Console.WriteLine("Inquiry VA Request: " + inquiryRequestBody);
         // Send POST request to create VA
        string externalId = SignatureGeneratorUtils.GenerateRandomNumberString(6);
        string inquiryResponse = await vaService.SendPostRequest(apiEndpoints.InquiryVA, accessToken, timestamp, inquiryRequestBody, externalId);

        //Assert.IsNotNull(inquiryResponse);
        Console.WriteLine("Inquiry VA Response: " + inquiryResponse);
    }
}

}