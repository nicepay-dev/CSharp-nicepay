using SignatureGenerator;

class RegistQris
{
    public async Task CreateQris_Test()
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

         var Bodybuilder = new NicepayRequestBuilder()
        .SetCommonFields(
            iMid: clientId,
            timeStamp: timestamp,
            payMethod: "08", // VA
            currency: "IDR",
            amt: amount,
            referenceNo: refNo,
            goodsNm: "Produk Test",
            merchantToken: merchantToken
        )
        .setMitraCd(mitraCd : "QSHP")
        .setShopId(shopId : "NICEPAY")
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
        
        .SetDbProcessUrl("https://merchant.com/callback")
        .SetCartData("");

        ApiEndpoints apiEndpoints = new ApiEndpoints();
        Dictionary<string, object> payload = Bodybuilder.Build();
        string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
        Console.WriteLine("Request Regist QRIS: " + jsonPayload);
        var registrationService = new NicepayRegistrationService(apiEndpoints,isProduction, isCloudServer);
        var result = await registrationService.SendPostAsync(apiEndpoints.RegistV2,payload);

        Console.WriteLine("Create Regist QRIS: " + result);
       
        // // Send POST request to ewallet payment
        // string externalId = SignatureGeneratorUtils.GenerateRandomNumberString(6);
        // string createResponse = await payoutService.SendPostRequest(apiEndpoints.CreatePayout, accessToken, timestamp, createRequestBody, externalId);

        // Console.WriteLine("Create Payout Response: " + createResponse);
    }
        
    }
    
    

