# C#-nicepay-snap
NICEPAY ❤️ C#
This is the Official C# API client/library for NICEPAY Payment API. More information about the product and see documentation at for more technical details.

## 1. Installation
### Using CLI 
``dotnet new console -n newProject 
  cd newProject
  dotnet run
```
## 2. Usage
### 2.1 Client Initialization and Configuration
Get your Credentials from [Nicepay Dashboard](https://bo.nicepay.co.id/)
Initialize Nicepay Config

```csharp
  public CredentialConfig()
    {
        ClientId = " YOUR_CLIENT_ID ";
        ClientSecret = " YOUR_CLIENT_SECRET ";
        PrivateKeyBase64 = " YOUR_PRIVATE_KEY ";
        ChannelId = "123456";
    }
```
You can inisiate the config

```csharp
   CredentialConfig config = new CredentialConfig();
 string clientId = config.ClientId;
 string clientSecret = config.ClientSecret;
 string privateKey = config.PrivateKey;
 string channelId = config.ChannelId;
```

### 2.2 Request for Access-Token

```csharp

using System.Text;

ApiEndpoints apiEndpoints = new ApiEndpoints();
            string url = apiEndpoints.AccessToken;

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("X-CLIENT-KEY", clientId);
            client.DefaultRequestHeaders.Add("X-SIGNATURE", signature);
            client.DefaultRequestHeaders.Add("X-TIMESTAMP", timestamp);

            var requestBody = new
            {
                grantType = "client_credentials",
                additionalInfo = new { }
            };


            string jsonRequestBody = System.Text.Json.JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
```

You can inisiate the Access Token

```csharp
var tokenRequester = new AccessTokenRequester();
string accessTokenResponse = await tokenRequester.GetAccessTokenAsync(clientId, signature, timestamp);
dynamic accessTokenObject = JObject.Parse(accessTokenResponse);
string accessToken = accessTokenObject.accessToken;
```

### 2.3 Request for Payment (i.e. Virtual Account)

```csharp
//Your previously initialized config
//ApiEndpoints apiEndpoints = new ApiEndpoints();
//VAService vaService = new VAService(apiEndpoints, clientSecret, clientId, channelId);
//RequestBodyGenerator requestBodyGenerator = new RequestBodyGenerator(clientId);

//Previously requested access-token
//var accessToken = ...

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

        string createResponse = await vaService.SendPostRequestCreate(apiEndpoints.CreateVA, accessToken, timestamp, createRequestBody, externalId + "02");
dynamic createVAResponse = JObject.Parse(createResponse);
String va_num = createVAResponse.virtualAccountData["virtualAccountNo"];
```

### 2.4 Verify signature notif 

```csharp
import io.github.nicepay.utils.SignatureUtils;

string stringToSignVer = "TNICEVA023|2024-08-19T17:12:40+07:00";
string publicKeyString = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEApizrKJl/1Legp3Zj8f0oTIjKnUWe2HJCBSoRsVLxtpf0Dr1MI+23y+AMNKKxVXxbvReZq/sD91uN4GFYMUr16LY9oX7nJXh9C1JlI4/Xb/Q9MF30o1XYvogHLATtvTR/KQ8hxrf6Nlj/yuzeqrT+PiQMZt1CaKiE6UMn36kq11DmDq4ocwcNhChKDudNZSZ4YYIFn5IgH05K+VsRjehpa0szbO8qHmvnprXVVcqvk7ZSS+6fYwDynOq0f552aL0LWX0glNhh9F0oJqmTreW4lM0mdhNDq4GhlJZl5IpaUiaGRM2Rz/t6spgwR7nqUhI9aE2kjzaorgP4ZWUGm3wlTwIDAQAB"; 

string signatureString = "VoxMPjbcV9pro4YyHGQgoRj4rDVJgYk2Ecxn+95B90w47Wnabtco35BfhGpR7a5RukUNnAdeOEBNczSFk4B9uYyu3jc+ceX+Dvz5OYSgSnw5CiMHtGiVnTAqCM/yHZ2MRpIEqekBc4BWMLVtexSWp0YEJjLyo9dZPrSkSbyLVuD7jkUbvmEpVdvK0uK15xb8jueCcDA6LYVXHkq/OMggS1/5mrLNriBhCGLuR7M7hBUJbhpOXSJJEy7XyfItTBA+3MRC2FLcvUpMDrn/wz1uH1+b9A6FP7mG0bRSBOm2BTLyf+xJR5+cdd88RhF70tNQdQxhqr4okVo3IFqlCz2FFg==";
bool isSignatureValid = SignatureGeneratorUtils.VerifySHA256RSA(stringToSignVer, publicKeyString, signatureString);
```

## 3. Other Samples
If you need samples for other payment methods and APIs, 
please refer to the test units on our [Repository](https://github.com/nicepay-dev/java-nicepay/tree/main/src/test/java/com/nicepay/client)

## Notes
#### Not Designed for Frontend Usage
This library is meant to be implemented on your backend server using C#.

## Get help

- [NICEPAY Docs](https://docs.nicepay.co.id/)
- [NICEPAY Dashboard ](https://bo.nicepay.co.id/)
- [SNAP documentation](https://docs.nicepay.co.id/nicepay-api-snap)
- Can't find answer you looking for? email to [cs@nicepay.co.id](mailto:cs@nicepay.co.id)

