using NUnit.Framework;
using SignatureGenerator;


namespace SignatureGenerator{
[TestFixture]
public class RegistVA
{

  [Test]
    public async Task CreateVA_Test()
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

         var Bodybuilder = new VirtualAccountBuilder()
        .SetVirtualAccount(
            iMid: clientId,
            timeStamp: timestamp,
            payMethod: "02", // VA
            currency: "IDR",
            amt: amount,
            referenceNo: refNo,
            goodsNm: "Produk Test",
            merchantToken: merchantToken,
            bankCd : "CENA",
            billingNm: "John Doe",
            billingPhone: "08123456789",
            billingEmail: "john@example.com",
            billingAddr: "Jl. Sudirman No.1",
            billingCity: "Jakarta",
            billingState: "DKI",
            billingPostCd: "12345",
            billingCountry: "ID",
            vacctValidDt : "", 
            vacctValidTm : "",
            dbProcessUrl : "https://merchant.com/callback",
            merFixAcctId : ""
        );
        
        ApiEndpoints apiEndpoints = new ApiEndpoints();
        Dictionary<string, object> payload = Bodybuilder.Build();
         string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
        Console.WriteLine("Request Regist VA: " + jsonPayload);
         var registrationService = new NicepayRegistrationService(apiEndpoints,isProduction, isCloudServer);
        var result = await registrationService.SendPostAsync(apiEndpoints.RegistV2,payload);

         Console.WriteLine("Create Regist VA: " + result);
    }
        
    }

}
    
    

