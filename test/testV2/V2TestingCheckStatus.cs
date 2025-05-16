using Newtonsoft.Json.Linq;
using SignatureGenerator;

class SetCheckStatus
{
    public async Task CheckStatus_Test()
    {
      TestingConstantService config = new TestingConstantService();
        string merchantKey = config.MerKey;
        string clientId = config.ClientId;
        string timestamp = DateTimeOffset.Now.ToString("yyyyMMddHHmmss");
        string refNo = "123";
        string amount = "1";
        bool isProduction = true;
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
        
        .SetCheckStatus(
          iMid : clientId,
          timeStamp : timestamp,
          tXid : "TNICEEW05105202504252356078508",
          amt : "1",
          referenceNo : "123",
          merchantToken: merchantToken
        );
      

        ApiEndpoints apiEndpoints = new ApiEndpoints();
        Dictionary<string, object> payload = Bodybuilder.Build();
        string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
        Console.WriteLine("Request Check Status: " + jsonPayload);
        var registrationService = new NicepayRegistrationService(apiEndpoints,isProduction, isCloudServer);
        var result = await registrationService.SendPostAsync(apiEndpoints.InquiryV2,payload);

         Console.WriteLine("Check Status Response: " + result);
       
       
      
    }
        
    }
    
    

