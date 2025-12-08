using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace SignatureGenerator
{
    [TestFixture]
    public class CancelV1
    {
        [Test]
        public async Task registDirect()
        {
            TestingConstantService config = new TestingConstantService();
            string merchantKey = config.MerKey;
            string clientId = config.ClientId;
            bool isProduction = false;
            bool isCloudServer = false;
            string amount = "10000";
            string txid = "IONPAYTEST02202512011059109610";

            var builder = new MerchantTokenBuilder()
                .SetIMid(clientId)
                .SetTXid(txid)
                .SetAmount(amount)
                .SetMerchantKey(merchantKey);

            string merchantToken = builder.BuildMerchantTokenCancelV1();
            Console.WriteLine("Create Merchant Token: " + merchantToken);
            

            var Bodybuilder = new V1Builder()
            .SetCancelV1(
                iMid: clientId,
                amt: amount,
                tXid : "IONPAYTEST02202512011059109610",
                payMethod: "02",
                cancelMsg : "Testing cancel",
                cancelType : "1",
                merchantToken: merchantToken
                
            );

            ApiEndpoints apiEndpoints = new ApiEndpoints();
            Dictionary<string, object> payload = Bodybuilder.Build();

            string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
            Console.WriteLine("Request Direct : " + jsonPayload);

            var registrationService = new NonSnapServices(apiEndpoints, isProduction, isCloudServer);

            
            var result = await registrationService.SendPostAsync(
                apiEndpoints.cancelV1,
                payload,
                useFormUrlEncoded: true
            );

            Console.WriteLine("Direct regist response: " + result);
            Console.WriteLine("==================================== ");
        }
    }
}
