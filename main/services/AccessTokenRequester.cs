
using System.Text;


namespace SignatureGenerator
{
    public class AccessTokenRequester
    {
        private static readonly HttpClient client = new HttpClient();
    
        public async Task<string> GetAccessTokenAsync(string clientId, string signature, string timestamp, bool isProduction, bool isCloudServer)
        {
            // URL API
           string _baseUrl = isCloudServer 
            ? (isProduction ? NICEPayBuilder.GetProductionCloud() : NICEPayBuilder.GetSandboxCloud()) 
            : (isProduction ? NICEPayBuilder.GetProductionBaseUrl() : NICEPayBuilder.GetSandboxBaseUrl());
            ApiEndpoints apiEndpoints = new ApiEndpoints();
            string url = _baseUrl + apiEndpoints.AccessToken;

             Console.WriteLine("URL " + url);

            // Membentuk header request
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("X-CLIENT-KEY", clientId);
            client.DefaultRequestHeaders.Add("X-SIGNATURE", signature);
            client.DefaultRequestHeaders.Add("X-TIMESTAMP", timestamp);

            Console.WriteLine("Signature " + signature);

            //Body Request
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
        }
    }
}
