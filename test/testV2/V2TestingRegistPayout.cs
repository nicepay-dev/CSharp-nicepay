using NUnit.Framework;
using SignatureGenerator;

namespace SignatureGenerator{

[TestFixture]
public class RegistPayout
{
      [Test]
    public async Task CreatePayout_Test()
    {
      TestingConstantPayout config = new TestingConstantPayout();
        string merchantKey = config.MerKey;
        string clientId = config.ClientId;
        string timestamp = DateTimeOffset.Now.ToString("yyyyMMddHHmmss");
        string refNo = SignatureGeneratorUtils.GenerateRandomNumberString(8);
        string amount = "10000";
        string accountNo = "08123456789";
        bool isProduction = false;
        bool isCloudServer = false;

        var builder = new MerchantTokenBuilder()
          .SetTimeStamp(timestamp)
          .SetIMid(clientId)
          .SetAmount(amount)
          .SetAccountNo(accountNo)
          .SetMerchantKey(merchantKey);

        // Act: Menghasilkan merchantToken
        string merchantToken = builder.BuildPayoutMerchantToken();
      
        Console.WriteLine("Create Merchant Token: " + merchantToken);
         var Bodybuilder = new NicepayRequestBuilder()
        
        .setBankCd(bankCd :"BNIN")
        .setPayout(
        accountNo : accountNo,
        benefNm:"IONPAY NETWORKS",
        benefStatus: "1",
        benefType :"1",
        reservedDt:"",
        reservedTm:"",
        benefPhone:"081211111111",
        payoutMethod:"",
        iMid: clientId,
        timeStamp: timestamp,
        amt: amount,
        referenceNo: refNo,
        merchantToken: merchantToken);
      

        ApiEndpoints apiEndpoints = new ApiEndpoints();
        Dictionary<string, object> payload = Bodybuilder.Build();
         string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
        Console.WriteLine("Request Regist Payout: " + jsonPayload);
         var registrationService = new NicepayRegistrationService(apiEndpoints,isProduction, isCloudServer);
        var result = await registrationService.SendPostAsync(apiEndpoints.RegistPayoutV2,payload);

         Console.WriteLine("Create Regist Payout: " + result);
       
      
    }
        
    }
    
}