using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SignatureGenerator;

class V2TestingCancel
{
    public async Task CancelV2_Testing()
    {
      TestingConstantService config = new TestingConstantService();
        string merchantKey = config.MerKey;
        string clientId = config.ClientId;
        string timestamp = DateTimeOffset.Now.ToString("yyyyMMddHHmmss");
        string amount = "10000";
        string txid = "NORMALTEST00202505071925213738";
        bool isProduction = false;
        bool isCloudServer = false;

        var builder = new MerchantTokenBuilder()
          .SetTimeStamp(timestamp)
          .SetIMid(clientId)
          .SetTXid(txid)
          .SetAmount(amount)
          .SetMerchantKey(merchantKey);

        // Act: Menghasilkan merchantToken
        string merchantToken = builder.BuildCancelMerchantToken();
      
        Console.WriteLine("Create Cancel Merchant Token : " + merchantToken);

      
        var Bodybuilder = new NicepayRequestBuilder()
        .SetCancel(
        iMid : clientId,
        timeStamp : timestamp,
        tXid : txid,
        amt : "10000",
        merchantToken : merchantToken,
        payMethod : "02",
        cancelType : "1",
        cancelMsg : "Testing cancel plugin c#", 
        cancelUserId : "",
        cancelServerIp : "", 
        cancelRetryCnt : "");
        

        ApiEndpoints apiEndpoints = new ApiEndpoints();
        Dictionary<string, object> payload = Bodybuilder.Build();
        string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
        Console.WriteLine("Request Cancel: " + jsonPayload);
        var registrationService = new NicepayRegistrationService(apiEndpoints,isProduction, isCloudServer);
        var result = await registrationService.SendPostAsync(apiEndpoints.CancelV2,payload);

         Console.WriteLine("Create Cancel: " + result);
       
    }
        
    }
    
    

