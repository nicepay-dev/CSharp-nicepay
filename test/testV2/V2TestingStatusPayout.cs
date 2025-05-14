using SignatureGenerator;

class CheckStatusPayout
{
    public async Task StatusPayout_Testing()
    {
      TestingConstantService config = new TestingConstantService();
        string merchantKey = config.MerKey;
        string clientId = config.ClientId;
        string timestamp = DateTimeOffset.Now.ToString("yyyyMMddHHmmss");
        string accountNo = "903327200";
        bool isProduction = false;
        bool isCloudServer = false;
        string txid = "";
        ApiEndpoints apiEndpoints = new ApiEndpoints();
        
        var builder1 = new MerchantTokenBuilder()
          .SetTimeStamp(timestamp)
          .SetIMid(clientId)
          .SetTXid(txid)
          .SetAccountNo(accountNo)
          .SetMerchantKey(merchantKey);

        // Act: Menghasilkan merchantToken
        string merchantToken1 = builder1.BuildPayoutStatusMerchantToken();
        var BodybuilderCheckStatus = new NicepayRequestBuilder()
        
        .SetPayoutCheckStatus(
          iMid : clientId,
          timeStamp : timestamp,
          merchantToken: merchantToken1,
          accountNo : accountNo,
          tXid : txid
        );
      
        Dictionary<string, object> payload1 = BodybuilderCheckStatus.Build();
        string jsonPayload1 = Newtonsoft.Json.JsonConvert.SerializeObject(payload1);
        Console.WriteLine("Request Check Status Payout: " + jsonPayload1);
        var checkStatusService = new NicepayRegistrationService(apiEndpoints,isProduction, isCloudServer);
        var result1 = await checkStatusService.SendPostAsync(apiEndpoints.InquiryPayoutV2,payload1);
        
        Console.WriteLine("Check Status Payout: " + result1);
      
    }
        
    }
    
    

