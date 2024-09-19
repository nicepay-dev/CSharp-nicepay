using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace SignatureGenerator
{
    public class VAService
    {

        private readonly ApiEndpoints _endpoints;
        private readonly string _clientSecret;
        private readonly string _clientId;
        private readonly string _channelId;

        public VAService(ApiEndpoints endpoints, string clientSecret, string clientId, string channelId)
        {
         _endpoints = endpoints;
        _clientSecret = clientSecret;
        _clientId = clientId;
        _channelId = channelId;
        }
        public async Task<string> SendPostRequestCreate(string endpoint, string accessToken, string timestamp, string requestBody, string externalId)
        {
            string signature = SignatureGeneratorUtils.GetSignature("POST", accessToken, requestBody, endpoint, timestamp, _clientSecret);
            string fullUrl = "https://dev.nicepay.co.id/nicepay/api/v1.0/transfer-va/create-va";
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-TIMESTAMP", timestamp);
            client.DefaultRequestHeaders.Add("X-SIGNATURE", signature);
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            client.DefaultRequestHeaders.Add("X-PARTNER-ID", _clientId);
            client.DefaultRequestHeaders.Add("X-EXTERNAL-ID", externalId);
            client.DefaultRequestHeaders.Add("CHANNEL-ID", _channelId);

        //string respbody = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(fullUrl, content);
        
        
        // Console.WriteLine("request Body: " + requestBody);
        // Console.WriteLine("signature : " + signature);
        // Console.WriteLine("timestamp: " + timestamp);
        // Console.WriteLine("_clientId : " + _clientId);
        // Console.WriteLine("externalId : " + externalId);
        // Console.WriteLine("channel : " + _channelId);
        // Console.WriteLine("https://dev.nicepay.co.id" + endpoint);
        string responseBody = await response.Content.ReadAsStringAsync();
        dynamic createVAResponse = JObject.Parse(responseBody);
        //Console.WriteLine(createVAResponse);
             // Log the response to check if it's a valid JSON
        //Console.WriteLine("Response Body: " + responseBody);

        return responseBody;
        }

         public async Task<string> SendPostRequestInquiry(string endpoint, string accessToken, string timestamp, string requestBody, string externalId)
        {
            string signature = SignatureGeneratorUtils.GetSignature("POST", accessToken, requestBody, endpoint, timestamp, _clientSecret);
            string fullUrl = "https://dev.nicepay.co.id/nicepay/api/v1.0/transfer-va/status";
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-TIMESTAMP", timestamp);
            client.DefaultRequestHeaders.Add("X-SIGNATURE", signature);
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            client.DefaultRequestHeaders.Add("X-PARTNER-ID", _clientId);
            client.DefaultRequestHeaders.Add("X-EXTERNAL-ID", externalId);
            client.DefaultRequestHeaders.Add("CHANNEL-ID", _channelId);

        //string respbody = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(fullUrl, content);
        
        
        // Console.WriteLine("request Body: " + requestBody);
        // Console.WriteLine("signature : " + signature);
        // Console.WriteLine("timestamp: " + timestamp);
        // Console.WriteLine("_clientId : " + _clientId);
        // Console.WriteLine("externalId : " + externalId);
        // Console.WriteLine("channel : " + _channelId);
        // Console.WriteLine("https://dev.nicepay.co.id" + endpoint);
        string responseBody = await response.Content.ReadAsStringAsync();
        dynamic inquiryResponse = JObject.Parse(responseBody);
        //Console.WriteLine(inquiryResponse);
             // Log the response to check if it's a valid JSON
        //Console.WriteLine("Response Body: " + responseBody);

        return responseBody;
        }

        public async Task<string> SendDeleteRequest(string endpoint, string accessToken, string timestamp, string requestBody, string externalId)
        {
            string signature = SignatureGeneratorUtils.GetSignature("DELETE", accessToken, requestBody, endpoint, timestamp, _clientSecret);
            string fullUrl = "https://dev.nicepay.co.id/nicepay/api/v1.0/transfer-va/delete-va";
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-TIMESTAMP", timestamp);
            client.DefaultRequestHeaders.Add("X-SIGNATURE", signature);
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            client.DefaultRequestHeaders.Add("X-PARTNER-ID", _clientId);
            client.DefaultRequestHeaders.Add("X-EXTERNAL-ID", externalId);
            client.DefaultRequestHeaders.Add("CHANNEL-ID", _channelId);

            // var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            // HttpResponseMessage response = await client.DeleteAsync("https://dev.nicepay.co.id" + endpoint,content);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(fullUrl),
                Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage response = await client.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }
    }
}