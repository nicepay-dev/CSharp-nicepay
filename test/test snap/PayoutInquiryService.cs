using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace SignatureGenerator{

[TestFixture]
public class PayoutInquiryService
{
    [Test]
    public async Task inquiryPayout_Test()
    {

        TestingConstant config = new TestingConstant();
        string clientId = config.ClientId;
        string privateKey = config.PrivateKey;
        string timestamp = DateTimeOffset.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
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
        string accessTokenResponse = await tokenRequester.GetAccessTokenAsync(clientId, signature, timestamp, isProduction, isCloudServer);
        dynamic accessTokenObject = JObject.Parse(accessTokenResponse);
        string accessToken = accessTokenObject.accessToken;
        Console.WriteLine("Inquiry Payout Akses token: " + accessToken);

        // Create VA request
        ApiEndpoints apiEndpoints = new ApiEndpoints();
        APIService payoutService = new APIService(apiEndpoints, clientSecret, clientId, channelId,isProduction, isCloudServer);
        

        // Jumlah item dan distribusi nilai `goodsAmt` ke setiap item
        
        var bodyBuilder = new InquiryBuilder()
        .SetInquiryPayoutSnap(
        originalPartnerReferenceNo :"IONPAYTEST07202412021513355940", 
        originalReferenceNo : "TSPYT54421487",
        merchantId:"IONPAYTEST",
        beneficiaryAccountNo :"5345000060"
        ).Build();
       
        // Send POST request to ewallet payment
        string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(bodyBuilder);
       Console.WriteLine("Inquiry Payout Request: " + jsonPayload);
        string externalId = SignatureGeneratorUtils.GenerateRandomNumberString(6);
        string createResponse = await payoutService.SendPostRequest(apiEndpoints.InquiryPayout, accessToken, timestamp, bodyBuilder, externalId);

        Console.WriteLine("Inquiry Payout Response: " + createResponse);
    }
}

}