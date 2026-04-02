using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace SignatureGenerator
{
    [TestFixture]
    public class RedirectTesting
    {
        [Test]
        public async Task registRedirect()
        {
            TestingConstantService config = new TestingConstantService();
            string merchantKey = config.MerKey;
            string clientId = config.ClientId;
            string timestamp = DateTimeOffset.Now.ToString("yyyyMMddHHmmss");
            bool isProduction = false;
            bool isCloudServer = false;
            string amount = "10000";
            string refNo = "854t6u5thr8hg";

            var builder = new MerchantTokenBuilder()
                .SetIMid(clientId)
                .SetRefNo(refNo)
                .SetAmount(amount)
                .SetMerchantKey(merchantKey);

            string merchantToken = builder.BuildMerchantTokenV1();
            Console.WriteLine("Create Merchant Token: " + merchantToken);

            var Bodybuilder = new V1Builder()
            .SetRedirect(
                timeStamp: timestamp,
                iMid: clientId,
                payMethod: "02",
                currency: "IDR",
                amt: amount,
                referenceNo: refNo,
                goodsNm: "Test Lib Java Redirect Regist",
                merchantToken: merchantToken,

                bankCd: "",
                billingNm: "Java Test",
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
                description: "Test transaction Java Lib",
                vat: "",
                fee: "",
                notaxAmt: "",
                deliveryNm: "Java Test",
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

                cartData: "{\"count\":\"1\",\"item\":[{\"goods_id\":\"BB12345678\",\"goods_detail\":\"BB12345678\",\"goods_name\":\"Pasar Modern\",\"goods_amt\":\"10000\",\"goods_type\":\"Sembako\",\"goods_url\":\"http://merchant.com/cellphones/iphone5s_64g,\",\"goods_quantity\":\"1\",\"goods_sellers_id\":\"SEL123\",\"goods_sellers_name\":\"Sellers 1\"}]}",
                sellers: "[{\"sellersId\": \"SEL123\",\"sellersNm\": \"Sellers 1\",\"sellersEmail\":\"sellers@test.com\",\"sellersAddress\": {\"sellerNm\": \"Sellers\",\"sellerLastNm\": \"1\",\"sellerAddr\": \"jalan berbangsa 1\",\"sellerCity\":\"Jakarta Barat\",\"sellerPostCd\": \"12344\",\"sellerPhone\":\"08123456789\",\"sellerCountry\": \"ID\"}}]",

                mitraCd: "ALMA",
                instmntType: "2",
                instmntMon: "1",
                recurrOpt: "1",

                payValidDt: "",
                payValidTm: "",
                paymentExpDt: "",
                paymentExpTm: "",
                shopId: "",
                onePassToken : "",
                cardCvv: ""
            );

            ApiEndpoints apiEndpoints = new ApiEndpoints();
            Dictionary<string, object> payload = Bodybuilder.Build();

            string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
            Console.WriteLine("Request Redirect : " + jsonPayload);

            var registrationService = new NonSnapServices(apiEndpoints, isProduction, isCloudServer);

            
            var result = await registrationService.SendPostAsync(
                apiEndpoints.RegistRedirect,
                payload,
                useFormUrlEncoded: true
            );

            Console.WriteLine("Redirect regist response: " + result);
            Console.WriteLine("==================================== ");
        }
    }
}
