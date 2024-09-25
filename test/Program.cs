// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace SignatureGenerator
{
    class Program
    {
        static async Task Main(string[] args)
        {
             TestingConstant config = new TestingConstant();
            string clientId = config.ClientId;
            string privateKey = config.PrivateKey;
            string timestamp = DateTimeOffset.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
            string clientSecret = config.ClientSecret;
            string channelId = "123456";
            string externalId = SignatureGeneratorUtils.GenerateRandomNumberString(6);
            bool isProduction = false;
            
            
            // Membuat signature
            var signatureGenerator = new SignatureGeneratorUtils();
            string stringToSign = signatureGenerator.GenerateStringToSign(clientId, timestamp);
            string signature = SignatureGeneratorUtils.GenerateSignature(privateKey, stringToSign);
            
            // Mendapatkan Access Token
            var tokenRequester = new AccessTokenRequester();
            string accessTokenResponse = await tokenRequester.GetAccessTokenAsync(clientId, signature, timestamp, isProduction);
            dynamic accessTokenObject = JObject.Parse(accessTokenResponse);
            string accessToken = accessTokenObject.accessToken;
            Console.WriteLine("Get The Access Token: " + accessToken);

            ApiEndpoints apiEndpoints = new ApiEndpoints();
            VAService vaService = new VAService(apiEndpoints, clientSecret, clientId, channelId, isProduction);
            RequestBodyGenerator requestBodyGenerator = new RequestBodyGenerator(clientId);
           

            // Membuat Request Body untuk Create VA
        string createRequestBody = requestBodyGenerator.GenerateCreateVARequest(
            customerNo: "",
            virtualAccountName: "John Test",
            trxId: "trxId" + timestamp,
            totalAmountValue: "10000.00",
            currency: "IDR",
            bankCd: "CENA",
            goodsNm: "Test",
            dbProcessUrl: "https://nicepay.co.id/"
        );

        string va_num = string.Empty;
        string amount = string.Empty;
        string trxID = string.Empty;
        string txidVa = string.Empty;
        // Proses Create VA (POST request dengan signature)
        string createResponse = await vaService.SendPostRequestCreate(apiEndpoints.CreateVA, accessToken, timestamp, createRequestBody, externalId + "02");
        Console.WriteLine("Create VA Response: " + createResponse);

        dynamic createVAResponse = JObject.Parse(createResponse);
        va_num = createVAResponse.virtualAccountData["virtualAccountNo"];
        amount = createVAResponse.virtualAccountData["totalAmount"]["value"].ToString();
        txidVa = createVAResponse.virtualAccountData["additionalInfo"]["tXidVA"].ToString();
        trxID = createVAResponse.virtualAccountData["trxId"].ToString();

            
            // Menghasilkan request body untuk Inquiry
         string inquiryRequestBody = requestBodyGenerator.GenerateInquiryRequest(
            customerNo: "",
            virtualAccountNo: va_num,
            inquiryRequestId: SignatureGeneratorUtils.GenerateRandomNumberString(6),
            totalAmountValue: amount,
            currency: "IDR",
            trxId: trxID,
            tXidVA: txidVa
        );

        // Proses Inquiry VA (POST request dengan signature)
        string inquiryResponse = await vaService.SendPostRequestInquiry(apiEndpoints.InquiryVA, accessToken, timestamp, inquiryRequestBody,externalId);
        Console.WriteLine("Inquiry VA Response: " + inquiryResponse);

        string stringToSignVer = "TNICEVA023|2024-08-19T17:12:40+07:00";
        string publicKeyString = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEApizrKJl/1Legp3Zj8f0oTIjKnUWe2HJCBSoRsVLxtpf0Dr1MI+23y+AMNKKxVXxbvReZq/sD91uN4GFYMUr16LY9oX7nJXh9C1JlI4/Xb/Q9MF30o1XYvogHLATtvTR/KQ8hxrf6Nlj/yuzeqrT+PiQMZt1CaKiE6UMn36kq11DmDq4ocwcNhChKDudNZSZ4YYIFn5IgH05K+VsRjehpa0szbO8qHmvnprXVVcqvk7ZSS+6fYwDynOq0f552aL0LWX0glNhh9F0oJqmTreW4lM0mdhNDq4GhlJZl5IpaUiaGRM2Rz/t6spgwR7nqUhI9aE2kjzaorgP4ZWUGm3wlTwIDAQAB"; // Kunci publik dalam Base64
        string signatureString = "VoxMPjbcV9pro4YyHGQgoRj4rDVJgYk2Ecxn+95B90w47Wnabtco35BfhGpR7a5RukUNnAdeOEBNczSFk4B9uYyu3jc+ceX+Dvz5OYSgSnw5CiMHtGiVnTAqCM/yHZ2MRpIEqekBc4BWMLVtexSWp0YEJjLyo9dZPrSkSbyLVuD7jkUbvmEpVdvK0uK15xb8jueCcDA6LYVXHkq/OMggS1/5mrLNriBhCGLuR7M7hBUJbhpOXSJJEy7XyfItTBA+3MRC2FLcvUpMDrn/wz1uH1+b9A6FP7mG0bRSBOm2BTLyf+xJR5+cdd88RhF70tNQdQxhqr4okVo3IFqlCz2FFg=="; // Tanda tangan dalam Base64

        Console.WriteLine("================================");
        bool isSignatureValid = SignatureGeneratorUtils.VerifySHA256RSA(stringToSignVer, publicKeyString, signatureString);

        Console.WriteLine("Apakah tanda tangan valid? " + isSignatureValid);



          //Membuat Request Body untuk Delete VA
        string deleteRequestBody = requestBodyGenerator.GenerateDeleteVARequest(
            virtualAccountNo: va_num,
            trxId: trxID,
            tXidVA: txidVa,
            totalAmount: amount
        );

        // Proses Delete VA (DELETE request dengan signature)
        string deleteResponse = await vaService.SendDeleteRequest(apiEndpoints.DeleteVA, accessToken, timestamp, deleteRequestBody, externalId + "03");
        Console.WriteLine("Delete VA Response: " + deleteResponse);



        }


        }
    }

