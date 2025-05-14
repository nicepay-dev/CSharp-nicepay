using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using SignatureGenerator;

namespace SignatureGenerator{

[TestFixture]
public class SnapEwalletService
{
    [Test]
    public async Task EwalletSnap_Test()
    {
TestingConstantService config = new TestingConstantService();
        string clientId = config.ClientId;
        string privateKey = config.PrivateKey;
        string timestamp = config.Timestamp;
        string clientSecret = config.ClientSecret;
        string channelId = "123456";
        string random = SignatureGeneratorUtils.GenerateRandomNumberString(8);
        string externalId = SignatureGeneratorUtils.GenerateRandomNumberString(6);
        bool isProduction = false;
        bool isCloudServer =  false;

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
        Console.WriteLine("Payment Ewallet Akses token: " + accessToken);

        // Create VA request
        ApiEndpoints apiEndpoints = new ApiEndpoints();
        APIService ewalletService = new APIService(apiEndpoints, clientSecret, clientId, channelId,isProduction, isCloudServer);

         var cartData = new
{
    count = "2",
    item = new[]
    {
        new {
            img_url = "https://d3nevzfk7ii3be.cloudfront.net/igi/vOrGHXlovukA566A.medium",
            goods_name = "Nokia 3360",
            goods_detail = "Old Nokia 3360",
            goods_amt = "5000.00",
            goods_quantity = "1"
        },
        new {
            img_url = "https://d3nevzfk7ii3be.cloudfront.net/igi/vOrGHXlovukA566A.medium",
            goods_name = "Nokia 3360",
            goods_detail = "Old Nokia 3360",
            goods_amt = "5000.00",
            goods_quantity = "1"
        }
    }
};
        var urlParams = new List<Dictionary<string, object>>
{
    new Dictionary<string, object>
    {
        { "url", "https://test1.bi.go.id/v1/test" },
        { "type", "PAY_NOTIFY" },
        { "isDeeplink", "Y" }
    },
    new Dictionary<string, object>
    {
        { "url", "https://test2.bi.go.id/v1/test" },
        { "type", "PAY_RETURN" },
        { "isDeeplink", "Y" }
    }
};

string cartDataJson = JsonConvert.SerializeObject(cartData);

var bodyBuilder = new EwalletBuilder()
    .SetEwalletSnap(
    partnerReferenceNo: "Refno"+random,
    merchantId: clientId,
    externalStoreId: "",
    value: "10000.00",
    currency: "IDR",
    subMerchantId : "",
    validUpTo : "",
    mitraCd: "DANA",
    goodsNm: "Testing Transaction Nicepay",
    billingNm: "IONPAY NETWORK TESTING",
    billingPhone: "089665542347",
    cartData: cartDataJson,
    dbProcessUrl: "https://webhook.site/c0ec8387-021b-4d1a-8a5f-adcd38763601",
    callBackUrl: "https://dev.nicepay.co.id/IONPAY_CLIENT/paymentResult.jsp",
    msId: "",
    msFee: "20",
    msFeeType: "2",
    mbFee: "20",
    mbFeeType: "2",
    urlParam : urlParams
    ).Build();

    
       string jsonPayload = JsonConvert.SerializeObject(bodyBuilder);
       Console.WriteLine("Payment Ewallet Request: " + jsonPayload);
        // Send POST request to ewallet payment
       
        string createResponse = await ewalletService.SendPostRequest(apiEndpoints.PaymentEwallet, accessToken, timestamp, bodyBuilder, externalId);

        Console.WriteLine("Payment Ewallet Response: " + createResponse);
    }
    }
}