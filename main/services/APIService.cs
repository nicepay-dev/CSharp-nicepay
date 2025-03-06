using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace SignatureGenerator
{
    public class APIService
    {

        private readonly ApiEndpoints _endpoints;
        private readonly string _clientSecret;
        private readonly string _clientId;
        private readonly string _channelId;
        private readonly bool _isProduction;
        private readonly bool _isCloudServer;

        public APIService(ApiEndpoints endpoints, string clientSecret, string clientId, string channelId, bool isProduction, bool isCloudServer)
        {
         _endpoints = endpoints;
        _clientSecret = clientSecret;
        _clientId = clientId;
        _channelId = channelId;
        _isProduction = isProduction;
        _isCloudServer = isCloudServer;
        }
        public async Task<string> SendPostRequest(string endpoint, string accessToken, string timestamp, string requestBody, string externalId)
        {
            string signature = SignatureGeneratorUtils.GetSignature("POST", accessToken, requestBody, endpoint, timestamp, _clientSecret);
            string fullUrl =  BuildFullUrl(endpoint);
            Console.WriteLine("endpoint: " + fullUrl);
            
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
        
        string responseBody = await response.Content.ReadAsStringAsync();
        dynamic createVAResponse = JObject.Parse(responseBody);

        return responseBody;
        }


        public async Task<string> SendDeleteRequest(string endpoint, string accessToken, string timestamp, string requestBody, string externalId)
        {
            string signature = SignatureGeneratorUtils.GetSignature("DELETE", accessToken, requestBody, endpoint, timestamp, _clientSecret);
           string fullUrl =  BuildFullUrl(endpoint);
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

         private string BuildFullUrl(string endpoint)
    {
        string baseUrl = _isCloudServer 
        ? (_isProduction ? NICEPayBuilder.GetProductionCloud() : NICEPayBuilder.GetSandboxCloud()) 
        : (_isProduction ? NICEPayBuilder.GetProductionBaseUrl() : NICEPayBuilder.GetSandboxBaseUrl());

        string fullUrl = new Uri(new Uri(baseUrl), endpoint).ToString();
        return fullUrl;

    }
     
    }
}
