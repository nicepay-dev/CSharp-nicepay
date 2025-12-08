using Newtonsoft.Json.Linq;
using SignatureGenerator;
using NUnit.Framework;


public class V2TestingBalanceInquiry
{
   [Test]
    public async Task BalanceInquiry_Test()
  {
    TestingConstantService config = new TestingConstantService();
    string merchantKey = "sQ8qaanOOuynFY5hXYIMZTVyu4GcWbMlvkjFhTocz+jyq5nXwK/KFT65uijOJ8hlNuOVhH1JpKSzCtVbmoRKQA==";
    string clientId = "SHANEN0001";
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

    var Bodybuilder = new V2Builder()
      .SetPayoutBalance(timeStamp: timestamp,
      iMid: clientId,
      merchantToken: merchantToken);


    ApiEndpoints apiEndpoints = new ApiEndpoints();
    Dictionary<string, object> payload = Bodybuilder.Build();
    string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
    Console.WriteLine("Request Balance Inquiry Payout: " + jsonPayload);
    var registrationService = new NonSnapServices(apiEndpoints, isProduction, isCloudServer);
    var result = await registrationService.SendPostAsync(apiEndpoints.BalanceInquiryV2, payload);

    Console.WriteLine("Balance Inquiry Payout: " + result);
    Console.WriteLine("==================================== ");

  }
        
    }
    
    

