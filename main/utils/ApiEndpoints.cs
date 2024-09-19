public class ApiEndpoints
{
    public string CreateVA { get; } = "/api/v1.0/transfer-va/create-va";
    public string InquiryVA { get; } = "/api/v1.0/transfer-va/status";
    public string DeleteVA { get; } = "/api/v1.0/transfer-va/delete-va";
    public string AccessToken { get; } = "https://dev.nicepay.co.id/nicepay/v1.0/access-token/b2b";
}