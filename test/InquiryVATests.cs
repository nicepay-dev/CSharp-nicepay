using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace SignatureGenerator{

[TestFixture]
public class InquiryVATests
{
    [Test]
    public async Task InquiryVA_Test()
    {
        TestingConstant config = new TestingConstant();
        string clientId = config.ClientId;
        string privateKey = config.PrivateKey;
        string timestamp = DateTimeOffset.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
        string clientSecret = config.ClientSecret;
        string channelId = "123456";
        bool isProduction = false;
        
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
        VAService vaService = new VAService(apiEndpoints, clientSecret, clientId, channelId,isProduction);
        RequestBodyGenerator requestBodyGenerator = new RequestBodyGenerator(clientId);
        
        // Add the value from regist
        string VA_NO = "9912304000009312";
        string trxID = "trxId64825778";
        string Amount = "230000.00";
        string txidVa = "TNICEVA02302202409230948272944";


        string inquiryRequestBody = requestBodyGenerator.GenerateInquiryRequest(
            customerNo: "",
            virtualAccountNo: VA_NO,
            inquiryRequestId: SignatureGeneratorUtils.GenerateRandomNumberString(6),
            totalAmountValue: Amount,
            currency: "IDR",
            trxId: trxID,
            tXidVA: txidVa
        );
         // Send POST request to create VA
        string externalId = SignatureGeneratorUtils.GenerateRandomNumberString(6);
        string inquiryResponse = await vaService.SendPostRequestInquiry(apiEndpoints.InquiryVA, accessToken, timestamp, inquiryRequestBody, externalId);

        //Assert.IsNotNull(inquiryResponse);
        Console.WriteLine("Inquiry VA Response: " + inquiryResponse);
    }
}

}