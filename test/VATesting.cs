using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace SignatureGenerator{

[TestFixture]
public class VATesting
{
    [Test,Order(1)]
    public async Task VA_Test()
    {

        string clientId = "TNICEVA023";
        string privateKey = "MIICdgIBADANBgkqhkiG9w0BAQEFAASCAmAwggJcAgEAAoGBAInJe1G22R2fMchIE6BjtYRqyMj6lurP/zq6vy79WaiGKt0Fxs4q3Ab4ifmOXd97ynS5f0JRfIqakXDcV/e2rx9bFdsS2HORY7o5At7D5E3tkyNM9smI/7dk8d3O0fyeZyrmPMySghzgkR3oMEDW1TCD5q63Hh/oq0LKZ/4Jjcb9AgMBAAECgYA4Boz2NPsjaE+9uFECrohoR2NNFVe4Msr8/mIuoSWLuMJFDMxBmHvO+dBggNr6vEMeIy7zsF6LnT32PiImv0mFRY5fRD5iLAAlIdh8ux9NXDIHgyera/PW4nyMaz2uC67MRm7uhCTKfDAJK7LXqrNVDlIBFdweH5uzmrPBn77foQJBAMPCnCzR9vIfqbk7gQaA0hVnXL3qBQPMmHaeIk0BMAfXTVq37PUfryo+80XXgEP1mN/e7f10GDUPFiVw6Wfwz38CQQC0L+xoxraftGnwFcVN1cK/MwqGS+DYNXnddo7Hu3+RShUjCz5E5NzVWH5yHu0E0Zt3sdYD2t7u7HSr9wn96OeDAkEApzB6eb0JD1kDd3PeilNTGXyhtIE9rzT5sbT0zpeJEelL44LaGa/pxkblNm0K2v/ShMC8uY6Bbi9oVqnMbj04uQJAJDIgTmfkla5bPZRR/zG6nkf1jEa/0w7i/R7szaiXlqsIFfMTPimvRtgxBmG6ASbOETxTHpEgCWTMhyLoCe54WwJATmPDSXk4APUQNvX5rr5OSfGWEOo67cKBvp5Wst+tpvc6AbIJeiRFlKF4fXYTb6HtiuulgwQNePuvlzlt2Q8hqQ==";
        string timestamp = DateTimeOffset.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
        string clientSecret = "1af9014925cab04606b2e77a7536cb0d5c51353924a966e503953e010234108a";
        string channelId = "123456";
        string random = SignatureGeneratorUtils.GenerateRandomNumberString(8);
        bool isProduction = false;

        // Generate signature
        var signatureGenerator = new SignatureGeneratorUtils();
        string stringToSign = signatureGenerator.GenerateStringToSign(clientId, timestamp);
        string signature = SignatureGeneratorUtils.GenerateSignature(privateKey, stringToSign);

        // Get access token
         var tokenRequester = new AccessTokenRequester();
        string accessTokenResponse = await tokenRequester.GetAccessTokenAsync(clientId, signature, timestamp, isProduction);
        dynamic accessTokenObject = JObject.Parse(accessTokenResponse);
        string accessToken = accessTokenObject.accessToken;

        // Create VA request
        ApiEndpoints apiEndpoints = new ApiEndpoints();
        VAService vaService = new VAService(apiEndpoints, clientSecret, clientId, channelId,isProduction);
        RequestBodyGenerator requestBodyGenerator = new RequestBodyGenerator(clientId);
        string createRequestBody = requestBodyGenerator.GenerateCreateVARequest(
            customerNo: "",
            virtualAccountName: "John Test",
            trxId: "trxId" + random,
            totalAmountValue: "230000.00",
            currency: "IDR",
            bankCd: "CENA",
            goodsNm: "Test",
            dbProcessUrl: "https://nicepay.co.id/"
        );
        string va_num = string.Empty;
        string amount = string.Empty;
        string trxID = string.Empty;
        string txidVa = string.Empty;
        // Send POST request to create VA
        string externalId = SignatureGeneratorUtils.GenerateRandomNumberString(6);
        string createResponse = await vaService.SendPostRequestCreate(apiEndpoints.CreateVA, accessToken, timestamp, createRequestBody, externalId);

        dynamic createVAResponse = JObject.Parse(createResponse);
        va_num = createVAResponse.virtualAccountData["virtualAccountNo"];
        amount = createVAResponse.virtualAccountData["totalAmount"]["value"].ToString();
        txidVa = createVAResponse.virtualAccountData["additionalInfo"]["tXidVA"].ToString();
        trxID = createVAResponse.virtualAccountData["trxId"].ToString();

        TestDataStore.VaNum = va_num;
        TestDataStore.TrxId = trxID;
        TestDataStore.Amount = amount;
        TestDataStore.TXidVA = txidVa;
        //Assert.NotNull(createResponse);
        Console.WriteLine($"VaNum: {TestDataStore.VaNum}");
        Console.WriteLine($"TrxId: {TestDataStore.TrxId}");
        Console.WriteLine($"Amount: {TestDataStore.Amount}");
        Console.WriteLine($"TXidVA: {TestDataStore.TXidVA}");
        Console.WriteLine("Create VA Response: " + createResponse);
    }

    [Test,Order(2)]
    public async Task InquiryVA_Test()
    {
        string clientId = "TNICEVA023";
        string privateKey = "MIICdgIBADANBgkqhkiG9w0BAQEFAASCAmAwggJcAgEAAoGBAInJe1G22R2fMchIE6BjtYRqyMj6lurP/zq6vy79WaiGKt0Fxs4q3Ab4ifmOXd97ynS5f0JRfIqakXDcV/e2rx9bFdsS2HORY7o5At7D5E3tkyNM9smI/7dk8d3O0fyeZyrmPMySghzgkR3oMEDW1TCD5q63Hh/oq0LKZ/4Jjcb9AgMBAAECgYA4Boz2NPsjaE+9uFECrohoR2NNFVe4Msr8/mIuoSWLuMJFDMxBmHvO+dBggNr6vEMeIy7zsF6LnT32PiImv0mFRY5fRD5iLAAlIdh8ux9NXDIHgyera/PW4nyMaz2uC67MRm7uhCTKfDAJK7LXqrNVDlIBFdweH5uzmrPBn77foQJBAMPCnCzR9vIfqbk7gQaA0hVnXL3qBQPMmHaeIk0BMAfXTVq37PUfryo+80XXgEP1mN/e7f10GDUPFiVw6Wfwz38CQQC0L+xoxraftGnwFcVN1cK/MwqGS+DYNXnddo7Hu3+RShUjCz5E5NzVWH5yHu0E0Zt3sdYD2t7u7HSr9wn96OeDAkEApzB6eb0JD1kDd3PeilNTGXyhtIE9rzT5sbT0zpeJEelL44LaGa/pxkblNm0K2v/ShMC8uY6Bbi9oVqnMbj04uQJAJDIgTmfkla5bPZRR/zG6nkf1jEa/0w7i/R7szaiXlqsIFfMTPimvRtgxBmG6ASbOETxTHpEgCWTMhyLoCe54WwJATmPDSXk4APUQNvX5rr5OSfGWEOo67cKBvp5Wst+tpvc6AbIJeiRFlKF4fXYTb6HtiuulgwQNePuvlzlt2Q8hqQ==";
        string timestamp = DateTimeOffset.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
        string clientSecret = "1af9014925cab04606b2e77a7536cb0d5c51353924a966e503953e010234108a";
        string channelId = "123456";
        bool isProduction = false;

        // Generate signature
        var signatureGenerator = new SignatureGeneratorUtils();
        string stringToSign = signatureGenerator.GenerateStringToSign(clientId, timestamp);
        string signature = SignatureGeneratorUtils.GenerateSignature(privateKey, stringToSign);

        // Get access token
        var tokenRequester = new AccessTokenRequester();
        string accessTokenResponse = await tokenRequester.GetAccessTokenAsync(clientId, signature, timestamp, isProduction);
        dynamic accessTokenObject = JObject.Parse(accessTokenResponse);
        string accessToken = accessTokenObject.accessToken;

        // Inquiry VA request
        ApiEndpoints apiEndpoints = new ApiEndpoints();
        VAService vaService = new VAService(apiEndpoints, clientSecret, clientId, channelId, isProduction);
        RequestBodyGenerator requestBodyGenerator = new RequestBodyGenerator(clientId);
        
        // Retrieve required data from previous VA creation response (mock this in real tests)
        string VA_NO = TestDataStore.VaNum;
        string trxID = TestDataStore.TrxId;
        string Amount =TestDataStore.Amount;
        string txidVa =  TestDataStore.TXidVA;


        string inquiryRequestBody = requestBodyGenerator.GenerateInquiryRequest(
            customerNo: "",
            virtualAccountNo: VA_NO,
            inquiryRequestId: SignatureGeneratorUtils.GenerateRandomNumberString(6),
            totalAmountValue: Amount,
            currency: "IDR",
            trxId: trxID,
            tXidVA: txidVa
        );
         // Send POST request to create VA
        string externalId = SignatureGeneratorUtils.GenerateRandomNumberString(6);
        string inquiryResponse = await vaService.SendPostRequestInquiry(apiEndpoints.InquiryVA, accessToken, timestamp, inquiryRequestBody, externalId);

        //Assert.IsNotNull(inquiryResponse);
        Console.WriteLine($"VaNum: {TestDataStore.VaNum}");
        Console.WriteLine($"TrxId: {TestDataStore.TrxId}");
        Console.WriteLine($"Amount: {TestDataStore.Amount}");
        Console.WriteLine($"TXidVA: {TestDataStore.TXidVA}");
        Console.WriteLine("Inquiry VA Response: " + inquiryResponse);
    }

    // [Test,Order(3)]
    // public async Task DeleteVA_Test()
    // {
    //     string clientId = "TNICEVA023";
    //     string privateKey = "MIICdgIBADANBgkqhkiG9w0BAQEFAASCAmAwggJcAgEAAoGBAInJe1G22R2fMchIE6BjtYRqyMj6lurP/zq6vy79WaiGKt0Fxs4q3Ab4ifmOXd97ynS5f0JRfIqakXDcV/e2rx9bFdsS2HORY7o5At7D5E3tkyNM9smI/7dk8d3O0fyeZyrmPMySghzgkR3oMEDW1TCD5q63Hh/oq0LKZ/4Jjcb9AgMBAAECgYA4Boz2NPsjaE+9uFECrohoR2NNFVe4Msr8/mIuoSWLuMJFDMxBmHvO+dBggNr6vEMeIy7zsF6LnT32PiImv0mFRY5fRD5iLAAlIdh8ux9NXDIHgyera/PW4nyMaz2uC67MRm7uhCTKfDAJK7LXqrNVDlIBFdweH5uzmrPBn77foQJBAMPCnCzR9vIfqbk7gQaA0hVnXL3qBQPMmHaeIk0BMAfXTVq37PUfryo+80XXgEP1mN/e7f10GDUPFiVw6Wfwz38CQQC0L+xoxraftGnwFcVN1cK/MwqGS+DYNXnddo7Hu3+RShUjCz5E5NzVWH5yHu0E0Zt3sdYD2t7u7HSr9wn96OeDAkEApzB6eb0JD1kDd3PeilNTGXyhtIE9rzT5sbT0zpeJEelL44LaGa/pxkblNm0K2v/ShMC8uY6Bbi9oVqnMbj04uQJAJDIgTmfkla5bPZRR/zG6nkf1jEa/0w7i/R7szaiXlqsIFfMTPimvRtgxBmG6ASbOETxTHpEgCWTMhyLoCe54WwJATmPDSXk4APUQNvX5rr5OSfGWEOo67cKBvp5Wst+tpvc6AbIJeiRFlKF4fXYTb6HtiuulgwQNePuvlzlt2Q8hqQ==";
    //     string timestamp = DateTimeOffset.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
    //     string clientSecret = "1af9014925cab04606b2e77a7536cb0d5c51353924a966e503953e010234108a";
    //     string channelId = "123456";

    //     // Generate signature
    //     var signatureGenerator = new SignatureGeneratorUtils();
    //     string stringToSign = signatureGenerator.GenerateStringToSign(clientId, timestamp);
    //     string signature = SignatureGeneratorUtils.GenerateSignature(privateKey, stringToSign);

    //     // Get access token
    //     var tokenRequester = new AccessTokenRequester();
    //     string accessTokenResponse = await tokenRequester.GetAccessTokenAsync(clientId, signature, timestamp);
    //     dynamic accessTokenObject = JObject.Parse(accessTokenResponse);
    //     string accessToken = accessTokenObject.accessToken;

    //     // Inquiry VA request
    //     ApiEndpoints apiEndpoints = new ApiEndpoints();
    //     VAService vaService = new VAService(apiEndpoints, clientSecret, clientId, channelId);
    //     RequestBodyGenerator requestBodyGenerator = new RequestBodyGenerator(clientId);
        
    //     // Retrieve required data from previous VA creation response (mock this in real tests)
    //     string VA_NO = TestDataStore.VaNum;
    //     string trxID = TestDataStore.TrxId;
    //     string Amount =TestDataStore.Amount;
    //     string txidVa =  TestDataStore.TXidVA;


    //     string deleteRequestBody = requestBodyGenerator.GenerateDeleteVARequest(
    //         virtualAccountNo: VA_NO,
    //         trxId: trxID,
    //         tXidVA: txidVa,
    //         totalAmount: Amount
    //     );

    //     // Proses Delete VA (DELETE request dengan signature)
    //      string externalId = SignatureGeneratorUtils.GenerateRandomNumberString(6);
    //     string deleteResponse = await vaService.SendDeleteRequest(apiEndpoints.DeleteVA, accessToken, timestamp, deleteRequestBody, externalId + "03");
    //     Console.WriteLine($"VaNum: {TestDataStore.VaNum}");
    //     Console.WriteLine($"TrxId: {TestDataStore.TrxId}");
    //     Console.WriteLine($"Amount: {TestDataStore.Amount}");
    //     Console.WriteLine($"TXidVA: {TestDataStore.TXidVA}");
    //     Console.WriteLine("Delete VA Response: " + deleteResponse);

    // }
}

}