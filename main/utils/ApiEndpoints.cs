public class ApiEndpoints
{ 
    // API Endpoints relative to the base URL
    public string CreateVA { get; } = "nicepay/api/v1.0/transfer-va/create-va";
    public string InquiryVA { get; } = "nicepay/api/v1.0/transfer-va/status";
    public string DeleteVA { get; } = "nicepay/api/v1.0/transfer-va/delete-va";
    public string AccessToken { get; } = "nicepay/v1.0/access-token/b2b";

     public static string GetSandboxBaseUrl()
    {
        return "https://dev.nicepay.co.id/";
    }

    public static string GetProductionBaseUrl()
    {
        return "https://www.nicepay.co.id/";
    }

    // Method to get the full URL based on the environment (sandbox/production)
    // public static string GetCreateVAUrl(bool isProduction) =>
    //     (isProduction ? ProductionBaseUrl : SandboxBaseUrl) + "api/v1.0/transfer-va/create-va";

    // public static string GetInquiryVAUrl(bool isProduction) =>
    //     (isProduction ? ProductionBaseUrl : SandboxBaseUrl) + "api/v1.0/transfer-va/status";

    // public static string GetDeleteVAUrl(bool isProduction) =>
    //     (isProduction ? ProductionBaseUrl : SandboxBaseUrl) + "api/v1.0/transfer-va/delete-va";

    // public static string GetAccessTokenUrl(bool isProduction) =>
    //     (isProduction ? ProductionBaseUrl : SandboxBaseUrl) + "nicepay/v1.0/access-token/b2b";
}