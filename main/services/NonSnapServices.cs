using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

public class NonSnapServices
{
    private readonly ApiEndpoints _endpoints;
    private readonly bool _isProduction;
    private readonly bool _isCloudServer;

    public NonSnapServices(ApiEndpoints endpoints, bool isProduction, bool isCloudServer)
    {
        _endpoints = endpoints;
        _isProduction = isProduction;
        _isCloudServer = isCloudServer;
    }

    public async Task<string> SendPostAsync(string endpoint, Dictionary<string, object> requestBody, bool useFormUrlEncoded = false)
    {
        if (requestBody == null || requestBody.Count == 0)
        {
            return "Error: Request body is null or empty.";
        }

        string fullUrl = BuildUrl(endpoint);
        Console.WriteLine("endpoint: " + fullUrl);

        try
        {
            using var client = new HttpClient();
            HttpContent content;

            if (useFormUrlEncoded)
            {
                // Convert ke x-www-form-urlencoded
                content = ConvertToFormUrlEncoded(requestBody);
            }
            else
            {
                // Default tetap JSON
                string jsonBody = JsonConvert.SerializeObject(requestBody);
                content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            }

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


    public async Task<string> SendOnePassTokenAsync(Dictionary<string, object> requestBody)
    {
        string fullUrl = BuildUrl(_endpoints.RegistCardTokenV1);
        Console.WriteLine("Request URL: " + fullUrl);

        using var client = new HttpClient();

        // Wajib: seluruh payload harus masuk ke jsonData
        string jsonString = JsonConvert.SerializeObject(requestBody);

        var form = new Dictionary<string, string>
        {
            { "jsonData", jsonString }
        };

        var content = new FormUrlEncodedContent(form);

        var response = await client.PostAsync(fullUrl, content);
        string responseText = await response.Content.ReadAsStringAsync();

        Console.WriteLine("Response: " + responseText);

        return responseText;
    }

    // Helper: Convert dictionary → x-www-form-urlencoded
    private FormUrlEncodedContent ConvertToFormUrlEncoded(Dictionary<string, object> data)
    {
        var formValues = new List<KeyValuePair<string, string>>();

        foreach (var kv in data)
        {
            formValues.Add(new KeyValuePair<string, string>(kv.Key, kv.Value?.ToString() ?? ""));
        }

        return new FormUrlEncodedContent(formValues);
    }

    private string BuildUrl(string endpoint)
    {
        string baseurl = _isCloudServer ?
            (_isProduction ? NICEPayBuilder.GetProductionCloud() : NICEPayBuilder.GetSandboxCloud()) :
            (_isProduction ? NICEPayBuilder.GetProductionBaseUrl() : NICEPayBuilder.GetSandboxBaseUrl());

        return new Uri(new Uri(baseurl), endpoint).ToString();
    }
}
