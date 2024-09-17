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
            CredentialConfig config = new CredentialConfig();
            string clientId = config.ClientId;
            string clientSecret = config.ClientSecret;
            string privateKey = config.PrivateKey;
            string channelId = config.ChannelId;
            //string clientId = "TNICEVA023";
            string timestamp = DateTimeOffset.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
            //string clientSecret = "1af9014925cab04606b2e77a7536cb0d5c51353924a966e503953e010234108a";
            //string privateKeyBase64 = "MIICdgIBADANBgkqhkiG9w0BAQEFAASCAmAwggJcAgEAAoGBAInJe1G22R2fMchIE6BjtYRqyMj6lurP/zq6vy79WaiGKt0Fxs4q3Ab4ifmOXd97ynS5f0JRfIqakXDcV/e2rx9bFdsS2HORY7o5At7D5E3tkyNM9smI/7dk8d3O0fyeZyrmPMySghzgkR3oMEDW1TCD5q63Hh/oq0LKZ/4Jjcb9AgMBAAECgYA4Boz2NPsjaE+9uFECrohoR2NNFVe4Msr8/mIuoSWLuMJFDMxBmHvO+dBggNr6vEMeIy7zsF6LnT32PiImv0mFRY5fRD5iLAAlIdh8ux9NXDIHgyera/PW4nyMaz2uC67MRm7uhCTKfDAJK7LXqrNVDlIBFdweH5uzmrPBn77foQJBAMPCnCzR9vIfqbk7gQaA0hVnXL3qBQPMmHaeIk0BMAfXTVq37PUfryo+80XXgEP1mN/e7f10GDUPFiVw6Wfwz38CQQC0L+xoxraftGnwFcVN1cK/MwqGS+DYNXnddo7Hu3+RShUjCz5E5NzVWH5yHu0E0Zt3sdYD2t7u7HSr9wn96OeDAkEApzB6eb0JD1kDd3PeilNTGXyhtIE9rzT5sbT0zpeJEelL44LaGa/pxkblNm0K2v/ShMC8uY6Bbi9oVqnMbj04uQJAJDIgTmfkla5bPZRR/zG6nkf1jEa/0w7i/R7szaiXlqsIFfMTPimvRtgxBmG6ASbOETxTHpEgCWTMhyLoCe54WwJATmPDSXk4APUQNvX5rr5OSfGWEOo67cKBvp5Wst+tpvc6AbIJeiRFlKF4fXYTb6HtiuulgwQNePuvlzlt2Q8hqQ==";
            //string channelId = "123456";
            string externalId = SignatureGeneratorUtils.GenerateRandomNumberString(6);
            
            
            // Membuat signature
            var signatureGenerator = new SignatureGeneratorUtils();
            string stringToSign = signatureGenerator.GenerateStringToSign(clientId, timestamp);
            string signature = SignatureGeneratorUtils.GenerateSignature(privateKey, stringToSign);
            
            // Mendapatkan Access Token
            var tokenRequester = new AccessTokenRequester();
            string accessTokenResponse = await tokenRequester.GetAccessTokenAsync(clientId, signature, timestamp);
            dynamic accessTokenObject = JObject.Parse(accessTokenResponse);
            string accessToken = accessTokenObject.accessToken;
            Console.WriteLine("Get The Access Token: " + accessToken);

            ApiEndpoints apiEndpoints = new ApiEndpoints();
            VAService vaService = new VAService(apiEndpoints, clientSecret, clientId, channelId);
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

