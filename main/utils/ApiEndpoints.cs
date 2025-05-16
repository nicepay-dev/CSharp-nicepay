public class ApiEndpoints
{ 
    // API Endpoints relative to the base URL
    public string CreateVA { get; } = "nicepay/api/v1.0/transfer-va/create-va";
    public string InquiryVA { get; } = "nicepay/api/v1.0/transfer-va/status";
    public string DeleteVA { get; } = "nicepay/api/v1.0/transfer-va/delete-va";

    public string PaymentEwallet { get; } = "nicepay/api/v1.0/debit/payment-host-to-host";
    public string StatusEwallet { get; } = "nicepay/api/v1.0/debit/status";
    public string RefundEwallet { get; } = "nicepay/api/v1.0/debit/refund";
    public string AccessToken { get; } = "nicepay/v1.0/access-token/b2b";


    public string GenerateQris { get; } = "nicepay/api/v1.0/qr/qr-mpm-generate";
    public string StatusQris { get; } = "nicepay/api/v1.0/qr/qr-mpm-query";
    public string RefundQris { get; } = "nicepay/api/v1.0/qr/qr-mpm-refund";


    public string CreatePayout { get; } = "nicepay/api/v1.0/transfer/registration";
    public string ApprovePayout { get; } = "nicepay/api/v1.0/transfer/approve";
    public string InquiryPayout { get; } = "nicepay/api/v1.0/transfer/inquiry";
    public string CancelPayout { get; } = "nicepay/api/v1.0/transfer/cancel";
    public string RejectPayout { get; } = "nicepay/api/v1.0/transfer/reject";
    public string BalancePayout { get; } = "nicepay/api/v1.0/balance-inquiry";


//DIRECT V2

    public string RegistV2 {get; } = "/nicepay/direct/v2/registration";
    public string InquiryV2 {get; } = "/nicepay/direct/v2/inquiry";
    public string CancelV2 {get; } = "/nicepay/direct/v2/cancel";
    public string RegistPayoutV2 {get; } = "/nicepay/api/direct/v2/requestPayout";
    public string InquiryPayoutV2 {get;} = "/nicepay/api/direct/v2/inquiryPayout";
    public string RejectPayoutV2 {get;} = "/nicepay/api/direct/v2/rejectPayout";
    public string ApprovePayoutV2 {get;} = "/nicepay/api/direct/v2/approvePayout";
    public string BalanceInquiryV2 {get;} = "/nicepay/api/direct/v2/balanceInquiry";


    //REDIRECT

     public string RegistRedirectV2 {get; } = "/nicepay/redirect/v2/registration";

    
}
