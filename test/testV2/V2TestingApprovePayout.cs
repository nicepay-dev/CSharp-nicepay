using Newtonsoft.Json.Linq;
using SignatureGenerator;

class V2TestingApprovePayout
{
    public async Task ApprovePayout_Test()
    {
      TestingConstantService config = new TestingConstantService();
        string merchantKey = config.MerKey;
        string clientId = config.ClientId;
        string timestamp = DateTimeOffset.Now.ToString("yyyyMMddHHmmss");
        string txid = "";
        bool isProduction = false;
        bool isCloudServer = false;

        var builder = new MerchantTokenBuilder()
          .SetTimeStamp(timestamp)
          .SetIMid(clientId)
          .SetTXid(txid)
          .SetMerchantKey(merchantKey);

        // Act: Menghasilkan merchantToken
        string merchantToken = builder.BuildPayoutStepMerchantToken();
      
      Console.WriteLine("Create Merchant Token: " + merchantToken);

      var Bodybuilder = new NicepayRequestBuilder()
        .SetPayoutStep(timeStamp :timestamp,
        iMid : clientId,
        tXid : "",
        merchantToken:merchantToken);
      

        ApiEndpoints apiEndpoints = new ApiEndpoints();
        Dictionary<string, object> payload = Bodybuilder.Build();
        string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
        Console.WriteLine("Request Approve Payout: " + jsonPayload);
        var registrationService = new NicepayRegistrationService(apiEndpoints,isProduction, isCloudServer);
        var result = await registrationService.SendPostAsync(apiEndpoints.ApprovePayoutV2,payload);

        Console.WriteLine("Approve Payout: " + result);
        Console.WriteLine("==================================== " );
      
    }
        
    }
    
    

