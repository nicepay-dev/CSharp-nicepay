using Newtonsoft.Json.Linq;
using SignatureGenerator;

class V2TestingBalanceInquiry
{
    public async Task BalanceInquiry_Test()
    {
      TestingConstantService config = new TestingConstantService();
        string merchantKey = config.MerKey;
        string clientId = config.ClientId;
        string timestamp = DateTimeOffset.Now.ToString("yyyyMMddHHmmss");
        bool isProduction = false;
        bool isCloudServer = false;

        var builder = new MerchantTokenBuilder()
          .SetTimeStamp(timestamp)
          .SetIMid(clientId)
          .SetMerchantKey(merchantKey);

        // Act: Menghasilkan merchantToken
        string merchantToken = builder.BuildPayoutStepMerchantToken();
      
      Console.WriteLine("Create Merchant Token: " + merchantToken);

      var Bodybuilder = new NicepayRequestBuilder()
        .SetPayoutBalance(timeStamp :timestamp,
        iMid : clientId,
        merchantToken:merchantToken);
      

        ApiEndpoints apiEndpoints = new ApiEndpoints();
        Dictionary<string, object> payload = Bodybuilder.Build();
        string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
        Console.WriteLine("Request Balance Inquiry Payout: " + jsonPayload);
        var registrationService = new NicepayRegistrationService(apiEndpoints,isProduction, isCloudServer);
        var result = await registrationService.SendPostAsync(apiEndpoints.BalanceInquiryV2,payload);

        Console.WriteLine("Balance Inquiry Payout: " + result);
        Console.WriteLine("==================================== " );
      
    }
        
    }
    
    

