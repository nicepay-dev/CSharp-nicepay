using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class NicepayRegistrationService
{
    // private readonly HttpClient _httpClient;

    private readonly ApiEndpoints _endpoints;
    private readonly bool _isProduction;
    private readonly bool _isCloudServer;

    // Optional constructor injection for easier testing
    public NicepayRegistrationService(ApiEndpoints endpoints,bool isProduction, bool isCloudServer)
    {      
         _endpoints = endpoints;
          _isProduction = isProduction;
        _isCloudServer = isCloudServer;
    }

    public async Task<string> SendPostAsync(string endpoint, Dictionary<string, object> requestBody)
    {
        if (requestBody == null || requestBody.Count == 0)
        {
            return "Error: Request body is null or empty.";
        }

        string fullUrl = BuildUrl(endpoint);
        Console.WriteLine("endpoint: " + fullUrl);
        // var url = "https://dev.nicepay.co.id/nicepay/direct/v2/registration";

        try
        {
            var jsonRequestBody = JsonConvert.SerializeObject(requestBody);

            using var content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");
           using var client = new HttpClient();
           var response = await client.PostAsync(fullUrl, content);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return responseContent;
            }
            else
            {
                return $"Error: {(int)response.StatusCode} - {response.ReasonPhrase}\nResponse: {responseContent}";
            }
        }
        catch (Exception ex)
        {
            return $"Exception occurred: {ex.Message}";
        }  
    }

    private string BuildUrl(string endpoint)
    {
        string baseurl = _isCloudServer ?
        (_isProduction ? NICEPayBuilder.GetProductionCloud() : NICEPayBuilder.GetSandboxCloud()):
        (_isProduction ? NICEPayBuilder.GetProductionBaseUrl() : NICEPayBuilder.GetSandboxBaseUrl());

        string fullUrl = new Uri(new Uri(baseurl),endpoint).ToString();
        return fullUrl;
    }
}
