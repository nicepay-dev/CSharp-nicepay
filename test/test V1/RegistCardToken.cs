using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace SignatureGenerator
{
    [TestFixture]
    public class RegistCardToken
    {
        [Test]
        public async Task registDirect()
        {
            TestingConstantService config = new TestingConstantService();
            string merchantKey = config.MerKey;
            string clientId = config.ClientId;
            string timestamp = DateTimeOffset.Now.ToString("yyyyMMddHHmmss");
            bool isProduction = false;
            bool isCloudServer = false;
            string amount = "10000";
            string refNo = "refNo" + timestamp;

            var builder = new MerchantTokenBuilder()
                .SetIMid(clientId)
                .SetRefNo(refNo)
                .SetAmount(amount)
                .SetMerchantKey(merchantKey);

            string merchantToken = builder.BuildMerchantTokenV1();
            Console.WriteLine("Create Merchant Token: " + merchantToken);
            

            var bodyBuilder = new V1Builder()
            .SetCardToken(
                iMid: clientId,
                cardNo: "5123450000000008",
                cardExpYymm: "2703",
                amt: amount,
                referenceNo: refNo,
                merchantToken: merchantToken
            ).Build();

            ApiEndpoints apiEndpoints = new ApiEndpoints();

             string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(bodyBuilder);

            Console.WriteLine("Request Direct : " + jsonPayload);

            var registrationService = new NonSnapServices(apiEndpoints, isProduction, isCloudServer);

            
            var result = await registrationService.SendOnePassTokenAsync(bodyBuilder);

            Console.WriteLine("Direct regist response: " + result);
            Console.WriteLine("==================================== ");
        }
    }
}
