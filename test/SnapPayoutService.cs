using NUnit.Framework;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using CreatePayout;

namespace SignatureGenerator{

[TestFixture]
public class SnapPayoutService
{
    [Test]
    public async Task createPayout_Test()
    {

        TestingConstantService config = new TestingConstantService();
        string clientId = config.ClientId;
        string privateKey = config.PrivateKey;
        string timestamp = DateTimeOffset.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"); 
        string clientSecret = config.ClientSecret;
        string channelId = "123456";
        string random = SignatureGeneratorUtils.GenerateRandomNumberString(8);
        bool isProduction = false;
        bool isCloudServer = true;
        string reservedDt = DateTime.Now.ToString("yyyyMMdd");
        string reservedTm = DateTime.Now.AddMinutes(70).ToString("HHmmss");

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
        Console.WriteLine("Create Payout Akses token: " + accessToken);

        // Create VA request
        ApiEndpoints apiEndpoints = new ApiEndpoints();
        APIService payoutService = new APIService(apiEndpoints, clientSecret, clientId, channelId,isProduction, isCloudServer);
        SnapPayoutServices snapPayoutServices = new SnapPayoutServices(clientId);
        
        string value = "1000.00";
        decimal totalValue = decimal.Parse(value);

        // Jumlah item dan distribusi nilai `goodsAmt` ke setiap item
        
        string createRequestBody = snapPayoutServices.GeneratePayoutRequest(
        merchantId : "IONPAYTEST", 
        beneficiaryAccountNo:"5345000060", 
        beneficiaryName:"IONPAY NETWORKS", 
        beneficiaryPhone:"08123456789",
        payoutMethod:"0", 
        beneficiaryBankCode:"CENA",  
        partnerReferenceNo:"TSPYT" + random,
        beneficiaryCustomerType:"1",
        beneficiaryCustomerResidence:"1", 
        reservedTm : reservedTm,
        reservedDt : reservedDt,
        value:value,
        currency:"IDR",
        description:"This is test Request C Sharp"
        );

        
        Console.WriteLine("Create Payout Request: " + createRequestBody);
        // Send POST request to ewallet payment
        string externalId = SignatureGeneratorUtils.GenerateRandomNumberString(6);
        string createResponse = await payoutService.SendPostRequest(apiEndpoints.CreatePayout, accessToken, timestamp, createRequestBody, externalId);

        Console.WriteLine("Create Payout Response: " + createResponse);
    }
}

}