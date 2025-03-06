using NUnit.Framework;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using CreateQris;

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
        Console.WriteLine("Generate Qris Akses token: " + accessToken);

        // Create VA request
        ApiEndpoints apiEndpoints = new ApiEndpoints();
        APIService qrisService = new APIService(apiEndpoints, clientSecret, clientId, channelId,isProduction, isCloudServer);
        SnapQrisServices snapQrisServices = new SnapQrisServices(clientId);
        
        string value = "500.00";
        decimal totalValue = decimal.Parse(value);

        // // Jumlah item dan distribusi nilai `goodsAmt` ke setiap item
        // var items = new List<CartItem>
        // {
        //     new CartItem
        //     {
        //         img_url = "http://img.aaa.com/ima1.jpg",
        //         goods_name = "Item 1 Name",
        //         goods_detail = "Item 1 Detail",
        //         goods_quantity = "1"
        //     },
        //     new CartItem
        //     {
        //         img_url = "http://img.aaa.com/ima2.jpg",
        //         goods_name = "Item 2 Name",
        //         goods_detail = "Item 2 Detail",
        //         goods_quantity = "1"
        //     }
        // };

        // // Menghitung `goodsAmt` per item (contoh distribusi rata)
        // decimal goodsAmtPerItem = totalValue / items.Count;

        // // Menyesuaikan `goodsAmt` setiap item
        // foreach (var item in items)
        // {
        //    item.goods_amt = "250.00"; // Format 2 desimal
        // }

        // // Membuat objek `cartData` dan mengonversi ke JSON
        // var cartData = new CartData
        // {
        //     count = items.Count.ToString(),
        //     item = items
        // };

        //string cartDataJson = JsonConvert.SerializeObject(cartData);

        //Console.WriteLine("Generate Qris Response: " + cartDataJson);
        string createRequestBody = snapQrisServices.GenerateQrisRequest(
        partnerReferenceNo: "ncpy" + random,
        merchantId: "TNICEQR081",
        storeId: "NicepayStoreID1",
        validityPeriod:"",
        amountValue: value,
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
        );
       
       Console.WriteLine("Generate Qris Request: " + createRequestBody);
        // Send POST request to qris payment
        string externalId = SignatureGeneratorUtils.GenerateRandomNumberString(6);
        string createResponse = await qrisService.SendPostRequest(apiEndpoints.GenerateQris, accessToken, timestamp, createRequestBody, externalId);

        Console.WriteLine("Generate Qris Response: " + createResponse);
    }
}

}