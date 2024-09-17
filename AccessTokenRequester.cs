
using System.Text;


namespace SignatureGenerator
{
    public class AccessTokenRequester
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<string> GetAccessTokenAsync(string clientId, string signature, string timestamp)
        {
            // URL API

            ApiEndpoints apiEndpoints = new ApiEndpoints();
            string url = apiEndpoints.AccessToken;

            // Membentuk header request
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("X-CLIENT-KEY", clientId);
            client.DefaultRequestHeaders.Add("X-SIGNATURE", signature);
            client.DefaultRequestHeaders.Add("X-TIMESTAMP", timestamp);

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
