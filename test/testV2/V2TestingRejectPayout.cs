using SignatureGenerator;

class V2TestingRejectPayout
{
    public async Task RejectPayout_Testing()
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
        Console.WriteLine("Request Reject Payout: " + jsonPayload);
        var registrationService = new NicepayRegistrationService(apiEndpoints,isProduction, isCloudServer);
        var result = await registrationService.SendPostAsync(apiEndpoints.RejectPayoutV2,payload);

        Console.WriteLine("Reject Payout: " + result);
        Console.WriteLine("==================================== " );
      
    }
        
    }
    
    

