using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace SignatureGenerator
{
    [TestFixture]
    public class PaymentCardV1
    {
        [Test]
        public async Task registDirect()
        {
            TestingConstantService config = new TestingConstantService();
            string merchantKey = config.MerKey;
            string clientId = config.ClientId;
            string timestamp = DateTimeOffset.Now.ToString("yyyyMMddHHmmss");
            bool isProduction = false;
            bool isCloudServer = false;
            string amount = "10000";
            string refNo = "refNo" + timestamp;

            var builder = new MerchantTokenBuilder()
                .SetIMid(clientId)
                .SetRefNo(refNo)
                .SetAmount(amount)
                .SetMerchantKey(merchantKey);

            string merchantToken = builder.BuildMerchantTokenV1();
            Console.WriteLine("Create Merchant Token: " + merchantToken);
            
            var cartData = new
{
    count = "2",
    item = new[]
    {
        new {
            img_url = "https://d3nevzfk7ii3be.cloudfront.net/igi/vOrGHXlovukA566A.medium",
            goods_name = "Nokia 3360",
            goods_detail = "Old Nokia 3360",
            goods_amt = amount,
            goods_quantity = "1"
        },
        new {
            img_url = "https://d3nevzfk7ii3be.cloudfront.net/igi/vOrGHXlovukA566A.medium",
            goods_name = "Nokia 3360",
            goods_detail = "Old Nokia 3360",
            goods_amt = "0",
            goods_quantity = "1"
        }
    }
};
            string cartDataJson = JsonConvert.SerializeObject(cartData);

            var Bodybuilder = new V1Builder()
            .SetRedirect(
                timeStamp: timestamp,
                iMid: clientId,
                payMethod: "01",
                currency: "IDR",
                amt: amount,
                referenceNo: refNo,
                goodsNm: "Test Lib Csharp direct Regist V1",
                merchantToken: merchantToken,

                bankCd: "BMRI",
                billingNm: "Csharp Test",
                billingPhone: "081234567890",
                billingEmail: "email@merchant.com",
                billingAddr: "",
                billingCity: "Jakarta Selatan",
                billingState: "DKI Jakarta",
                billingPostCd: "12345",
                billingCountry: "Indonesia",

                vacctValidDt: "",
                vacctValidTm: "",

                dbProcessUrl: "https://merchant.com/dbProcessUrl",
                merFixAcctId: "",

                callBackUrl: "https://dev.nicepay.co.id/IONPAY_CLIENT/paymentResult.jsp",
                description: "Test transaction Csharp Lib",
                vat: "",
                fee: "",
                notaxAmt: "",
                deliveryNm: "Csharp Test",
                deliveryPhone: "081234567890",
                deliveryAddr: "",
                deliveryCity: "Jakarta Selatan",
                deliveryState: "DKI Jakarta",
                deliveryPostCd: "12345",
                deliveryCountry: "Indonesia",

                reqDt: "",
                reqTm: "",
                reqDomain: "https://www.merchant.com",
                reqServerIP: "127.0.0.1",
                reqClientVer: "",
                userIP: "127.0.0.1",
                userSessionID: "697D6922C961070967D3BA1BA5699C2C",
                userAgent: "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML,like Gecko) Chrome/60.0.3112.101 Safari/537.36",
                userLanguage: "ko-KR,en-US;q=0.8,ko;q=0.6,en;q=0.4",

                cartData: cartDataJson,
                sellers: "[{\"sellersId\": \"SEL123\",\"sellersNm\": \"Sellers 1\",\"sellersEmail\":\"sellers@test.com\",\"sellersAddress\": {\"sellerNm\": \"Sellers\",\"sellerLastNm\": \"1\",\"sellerAddr\": \"jalan berbangsa 1\",\"sellerCity\":\"Jakarta Barat\",\"sellerPostCd\": \"12344\",\"sellerPhone\":\"08123456789\",\"sellerCountry\": \"ID\"}}]",

                mitraCd: "DANA",
                instmntType: "1",
                instmntMon: "1",
                recurrOpt: "1",

                payValidDt: "",
                payValidTm: "",
                paymentExpDt: "",
                paymentExpTm: "",
                shopId: "",
                onePassToken : "f6bac4ce8052fcef808dad9009a16824f330f0accbf0cdb157f3e814f114ef34",
                cardCvv: "100"
            );

            ApiEndpoints apiEndpoints = new ApiEndpoints();
            Dictionary<string, object> payload = Bodybuilder.Build();

            string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
            Console.WriteLine("Request Direct : " + jsonPayload);

            var registrationService = new NonSnapServices(apiEndpoints, isProduction, isCloudServer);

            
            var result = await registrationService.SendPostAsync(
                apiEndpoints.RegistDirectV1,
                payload,
                useFormUrlEncoded: true
            );

            Console.WriteLine("Direct regist response: " + result);
            Console.WriteLine("==================================== ");
        }
    }
}
