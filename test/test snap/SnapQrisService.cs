using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace SignatureGenerator{

[TestFixture]
public class SnapQrisService
{
    [Test]
    public async Task generateQris_Test()
    {

        TestingConstant config = new TestingConstant();
        string clientId = config.ClientId;
        string privateKey = config.PrivateKey;
        string timestamp = DateTimeOffset.Now.AddMinutes(5).ToString("yyyy-MM-ddTHH:mm:sszzz");
        string clientSecret = config.ClientSecret;
        string channelId = "123456";
        string random = SignatureGeneratorUtils.GenerateRandomNumberString(8);
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
        Console.WriteLine("Generate Qris Akses token: " + accessToken);

        // Create VA request
        ApiEndpoints apiEndpoints = new ApiEndpoints();
        APIService qrisService = new APIService(apiEndpoints, clientSecret, clientId, channelId,isProduction, isCloudServer);
        // SnapQrisServices snapQrisServices = new SnapQrisServices(clientId);
        
        string value = "500.00";
        decimal totalValue = decimal.Parse(value);

        
        //Console.WriteLine("Generate Qris Response: " + cartDataJson);
        var bodyBuilder = new QrisBuilder()
        .SetQrisSnap(
        partnerReferenceNo: "ncpy" + random,
        merchantId: "TNICEQR081",
        storeId: "NICEPAY",
        validityPeriod:"",
        value: value,
        currency: "IDR",
        goodsNm: "testing",
        billingNm: "testing",
        billingPhone: "12345678",
        billingEmail: "merchant@email.co",
        billingCity: "Jakarta",
        billingState: "Indonesia",
        billingPostCd: "14234",
        billingCountry: "indonesia",
        callBackUrl: "https://ptsv2.com/t/qa-testing-qris/post",
        dbProcessUrl: "https://ptsv2.com/t/qa-testing-qris/post",
        mitraCd: "QSHP",
        userIP: "127.0.0.1",
        cartData: "{\"count\":\"1\",\"item\":[{\"img_url\":\"http://www.jamgora.com/media/avatar/noimage.png\",\"goods_detail\":\"BB12345678\",\"goods_name\":\"Pasar Modern\",\"goods_amt\":\"10000\",\"goods_quantity\":\"1\"}]}"
        ).Build();

        string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(bodyBuilder);
        Console.WriteLine("Generate Qris Request: " + jsonPayload);
        // Send POST request to qris payment
        string externalId = SignatureGeneratorUtils.GenerateRandomNumberString(6);
        string createResponse = await qrisService.SendPostRequest(apiEndpoints.GenerateQris, accessToken, timestamp, bodyBuilder, externalId);

        Console.WriteLine("Generate Qris Response: " + createResponse);
    }
}

}