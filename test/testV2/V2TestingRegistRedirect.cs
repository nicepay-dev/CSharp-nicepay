using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SignatureGenerator;

class V2TestingRegistRedirect
{
    public async Task RegistRedirect_Test()
    {
      TestingConstantService config = new TestingConstantService();
        string merchantKey = config.MerKey;
        string clientId = config.ClientId;
        string timestamp = DateTimeOffset.Now.ToString("yyyyMMddHHmmss");
        string refNo = "ORD" + SignatureGeneratorUtils.GenerateRandomNumberString(8);
        string amount = "10000";
        bool isProduction = false;
        bool isCloudServer = false;

        var builder = new MerchantTokenBuilder()
          .SetTimeStamp(timestamp)
          .SetIMid(clientId)
          .SetRefNo(refNo)
          .SetAmount(amount)
          .SetMerchantKey(merchantKey);

        // Act: Menghasilkan merchantToken
        string merchantToken = builder.BuildMerchantToken();
      
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
         var Bodybuilder = new NicepayRequestBuilder()

         // BASE REQUEST MANDATORY
        .SetCommonFields(
            iMid: clientId,
            timeStamp: timestamp,
            payMethod: "07", 
            currency: "IDR",
            amt: amount,
            referenceNo: refNo,
            goodsNm: "Produk Test",
            merchantToken: merchantToken
        )
        .SetUserInfo(userIP : "127.0.0.1", userSessionID : "", userAgent : "", userLanguage: "")
        .SetCallBackUrl(callBackUrl : "https://www.nicepay.co.id/IONPAY_CLIENT/paymentResult.jsp")
        .SetDbProcessUrl("https://merchant.com/callback")
        .SetCartData(cartData: cartDataJson)
        .SetBillingInfo(
            billingNm: "John Doe",
            billingPhone: "08123456789",
            billingEmail: "john@example.com",
            billingAddr: "Jl. Sudirman No.1",
            billingCity: "Jakarta",
            billingState: "DKI",
            billingPostCd: "12345",
            billingCountry: "ID"
        )
        // BASE REQUEST

        // FOR VA
        .setBankCd(bankCd : "CENA")
        .SetVaExpiry(
        vacctValidDt : "", 
        vacctValidTm :"")

        
        // FOR EWALLET
        .setMitraCd(mitraCd : "ESHP")
        

        //FOR CC
        .setCreditCard(
        instmntType : "",
        instmntMon : "", 
        recurrOpt :"")

        //FOR QRIS
        .setPaymentExp(
        paymentExpDt : "", 
        paymentExpTm : ""
        )

        .SetDeliveryInfo(
            deliveryNm: "John Doe",
            deliveryPhone: "08123456789",
            deliveryAddr: "Jl. Sudirman No.1",
            deliveryCity: "Jakarta",
            deliveryState: "DKI",
            deliveryPostCd: "12345",
            deliveryCountry: "ID"
        );
        

        ApiEndpoints apiEndpoints = new ApiEndpoints();
        Dictionary<string, object> payload = Bodybuilder.Build();
         string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
        Console.WriteLine("Request Regist Redirect: " + jsonPayload);
         var registrationService = new NicepayRegistrationService(apiEndpoints,isProduction, isCloudServer);
        var result = await registrationService.SendPostAsync(apiEndpoints.RegistRedirectV2,payload);

         Console.WriteLine("Create Regist Redirect: " + result);
        string finalUrl = SignatureGeneratorUtils.GeneratePaymentUrl(result);

        Console.WriteLine("========================== " );
        Console.WriteLine("Final Payment URL: " + finalUrl);
        Console.WriteLine("========================== " );
    }
        
    }
    
    

