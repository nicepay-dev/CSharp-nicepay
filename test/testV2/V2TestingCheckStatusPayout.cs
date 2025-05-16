using Newtonsoft.Json.Linq;
using SignatureGenerator;

class V2TestingCheckStatusPayout
{
    public async Task CheckStatusPayout_Test()
    {
      TestingConstantService config = new TestingConstantService();
        string merchantKey = config.MerKey;
        string clientId = config.ClientId;
        string timestamp = DateTimeOffset.Now.ToString("yyyyMMddHHmmss");
        string refNo = SignatureGeneratorUtils.GenerateRandomNumberString(8);
        string accountNo = "903327200";
        bool isProduction = true;
        bool isCloudServer = false;

       

        var builder1 = new MerchantTokenBuilder()
          .SetTimeStamp(timestamp)
          .SetIMid(clientId)
          .SetTXid("")
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
          tXid : ""
        );
        ApiEndpoints apiEndpoints = new ApiEndpoints();
        Dictionary<string, object> payload1 = BodybuilderCheckStatus.Build();
        string jsonPayload1 = Newtonsoft.Json.JsonConvert.SerializeObject(payload1);
        Console.WriteLine("Request Check Status Payout: " + jsonPayload1);
        var checkStatusService = new NicepayRegistrationService(apiEndpoints,isProduction, isCloudServer);
        var result1 = await checkStatusService.SendPostAsync(apiEndpoints.InquiryPayoutV2,payload1);
        
        Console.WriteLine("Check Status Payout: " + result1);
      
    }
        
    }
    
    

