using Newtonsoft.Json;
using SignatureGenerator;

class RegistEwallet
{
    public async Task CreateEwallet_Test()
    {
      TestingConstantService config = new TestingConstantService();
        string merchantKey = config.MerKey;
        string clientId = config.ClientId;
        string timestamp = DateTimeOffset.Now.ToString("yyyyMMddHHmmss");
        string refNo = SignatureGeneratorUtils.GenerateRandomNumberString(8);
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
          Console.WriteLine(cartData);
         var Bodybuilder = new NicepayRequestBuilder()
        .SetCommonFields(
            iMid: clientId,
            timeStamp: timestamp,
            payMethod: "05", 
            currency: "IDR",
            amt: amount,
            referenceNo: refNo,
            goodsNm: "Produk Test",
            merchantToken: merchantToken
        )
        .setMitraCd(mitraCd : "ESHP")
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
        .SetDeliveryInfo(
            deliveryNm: "John Doe",
            deliveryPhone: "08123456789",
            deliveryAddr: "Jl. Sudirman No.1",
            deliveryCity: "Jakarta",
            deliveryState: "DKI",
            deliveryPostCd: "12345",
            deliveryCountry: "ID"
        )
        
        .SetDbProcessUrl("https://merchant.com/callback")
        .SetCartData(cartData: cartDataJson);

        ApiEndpoints apiEndpoints = new ApiEndpoints();
        Dictionary<string, object> payload = Bodybuilder.Build();
         string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
        Console.WriteLine("Request Regist Ewallet: " + jsonPayload);
         var registrationService = new NicepayRegistrationService(apiEndpoints,isProduction, isCloudServer);
        var result = await registrationService.SendPostAsync(apiEndpoints.RegistV2,payload);

         Console.WriteLine("Create Regist Ewallet: " + result);
       
      
    }
        
    }
    
    

