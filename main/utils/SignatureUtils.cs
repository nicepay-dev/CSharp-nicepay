
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SignatureGenerator;
public class SignatureGeneratorUtils()
{

     public string GenerateStringToSign(string clientId, string timestamp)
        {
            
            return clientId + "|" + timestamp;
        }

    //Generate signature Akses Token
    public static string GenerateSignature(string privateKeyBase64, string clientId, string timestamp)
        {
           
           string stringToSign =  clientId + "|" + timestamp;
            byte[] privateKeyBytes = Convert.FromBase64String(privateKeyBase64);

           using RSA rsa = RSA.Create();
            rsa.ImportPkcs8PrivateKey(privateKeyBytes, out _);

            byte[] data = Encoding.UTF8.GetBytes(stringToSign);
            byte[] signatureBytes = rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            return Convert.ToBase64String(signatureBytes);
        }
   public static string GetSignature(string httpMethod, string accessToken, Dictionary<string, object> requestBody, string endpoint, string timeStamp, string clientSecret)
    {
        string jsonBody = JsonConvert.SerializeObject(requestBody);      
        string hashedRequestBody = Sha256EncodeHex(jsonBody);
        Console.WriteLine("hasil hash " + hashedRequestBody);
        string endpointSign = endpoint.Replace("nicepay", "");

        string stringToSign = $"{httpMethod}:{endpointSign}:{accessToken}:{hashedRequestBody}:{timeStamp}";
        
        Console.WriteLine("String To sign : " + stringToSign);
        Console.WriteLine("================================");

        string sign = HmacSha512EncodeBase64(clientSecret, stringToSign);
        Console.WriteLine("Generated Signature : " + sign);

        return sign;
    }

    private static string HmacSha512EncodeBase64(string key, string data)
    {
        using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key)))
        {
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            return Convert.ToBase64String(hash);
        }
    }

    private static string Sha256EncodeHex(string data)
    {
        using (var sha256 = SHA256.Create())
        {
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }


    public static bool VerifySHA256RSA(string stringToSign, string publicKeyString, string signatureString)
    {
        bool isVerified = false;
        try
        {
            byte[] publicKeyBytes = Convert.FromBase64String(publicKeyString);
            byte[] signatureBytes = Convert.FromBase64String(signatureString);
            byte[] stringToSignBytes = Encoding.UTF8.GetBytes(stringToSign);

            using (RSA rsa = RSA.Create())
            {
  
                rsa.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);

    
                isVerified = rsa.VerifyData(
                    stringToSignBytes,
                    signatureBytes,
                    HashAlgorithmName.SHA256,
                    RSASignaturePadding.Pkcs1);
            }

            Console.WriteLine("Signature is " + (isVerified ? "valid" : "invalid"));
        }
        catch (Exception e)
        {
            Console.WriteLine("Error Generate Signature = " + e.Message);
        }
        return isVerified;
    }


    public static string GenerateRandomNumberString(int length)
    {
        Random random = new Random();
        string randomNumberString = string.Empty;

        for (int i = 0; i < length; i++)
        {
            randomNumberString += random.Next(0, 10).ToString();
        }

        return randomNumberString;
    }

    public static class SHA256Util
{
    public static string Encrypt(string value)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(value));
            StringBuilder result = new StringBuilder();
            foreach (byte b in bytes)
            {
                result.Append(b.ToString("x2"));
            }
            return result.ToString();
        }
    }
}


public static string GeneratePaymentUrl(string jsonResponse)
    {
        try
        {
            JObject json = JObject.Parse(jsonResponse);
            // Mengambil tXid dan paymentURL dari response
            string tXid = json["tXid"]?.ToString();
            string paymentUrl = json["paymentURL"]?.ToString();

            // Membuat URL lengkap untuk pembayaran
            return $"{paymentUrl}?tXid={tXid}";
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error parsing JSON: " + ex.Message);
            return null;
        }
    }

    

}
