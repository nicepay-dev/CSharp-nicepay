using NUnit.Framework;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using CreateEwallet;

namespace SignatureGenerator{

[TestFixture]
public class SnapEwalletService
{
    [Test]
    public async Task PaymentEwallet_Test()
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
        Console.WriteLine("Payment Ewallet Akses token: " + accessToken);

        // Create VA request
        ApiEndpoints apiEndpoints = new ApiEndpoints();
        APIService ewalletService = new APIService(apiEndpoints, clientSecret, clientId, channelId,isProduction);
        SnapEwalletServices snapEwalletServices = new SnapEwalletServices(clientId);
        
        string value = "500.00";
        decimal totalValue = decimal.Parse(value);

        // Jumlah item dan distribusi nilai `goodsAmt` ke setiap item
        var items = new List<CartItem>
        {
            new CartItem
            {
                img_url = "http://img.aaa.com/ima1.jpg",
                goods_name = "Item 1 Name",
                goods_detail = "Item 1 Detail",
                goods_quantity = "1"
            },
            new CartItem
            {
                img_url = "http://img.aaa.com/ima2.jpg",
                goods_name = "Item 2 Name",
                goods_detail = "Item 2 Detail",
                goods_quantity = "1"
            }
        };

        // Menghitung `goodsAmt` per item (contoh distribusi rata)
        decimal goodsAmtPerItem = totalValue / items.Count;

        // Menyesuaikan `goodsAmt` setiap item
        foreach (var item in items)
        {
           item.goods_amt = "250.00"; // Format 2 desimal
        }

        // Membuat objek `cartData` dan mengonversi ke JSON
        var cartData = new CartData
        {
            count = items.Count.ToString(),
            item = items
        };

        string cartDataJson = JsonConvert.SerializeObject(cartData);

        Console.WriteLine("Payment Ewallet Response: " + cartDataJson);
        string createRequestBody = snapEwalletServices.GenerateEwalletRequest(
        partnerReferenceNo: "RefnoTrxSHP3823768",
    merchantId: clientId,
    externalStoreId: "",
    value: value,
    currency: "IDR",
    mitraCd: "DANA",
    goodsNm: "Testing Transaction Nicepay",
    billingNm: "IONPAY NETWORK TESTING ",
    billingPhone: "089665542347",
    cartData:cartDataJson ,
    dbProcessUrl: "https://webhook.site/c0ec8387-021b-4d1a-8a5f-adcd38763601",
    callBackUrl: "https://dev.nicepay.co.id/IONPAY_CLIENT/paymentResult.jsp",
    msId: "",
    msFee: "20",
    msFeeType: "2",
    mbFee: "20",
    mbFeeType: "2"
        );
       
        // Send POST request to ewallet payment
        string externalId = SignatureGeneratorUtils.GenerateRandomNumberString(6);
        string createResponse = await ewalletService.SendPostRequest(apiEndpoints.PaymentEwallet, accessToken, timestamp, createRequestBody, externalId);

        Console.WriteLine("Payment Ewallet Response: " + createResponse);
    }
}

}