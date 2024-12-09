
using System.Dynamic;

namespace RefundQris;


public class refundRequest
{
    public string originalReferenceNo { get; set; }
    public string originalPartnerReferenceNo { get; set; }
    public string partnerRefundNo { get; set; }
    public string merchantId { get; set; }
    public string externalStoreId { get; set; }
    public Amount refundAmount { get; set; }
    public string reason { get; set; }
    public AdditionalInfo additionalInfo { get; set; }
}

public class Amount
{
    public string value { get; set; }
    public string currency { get; set; }
}

public class AdditionalInfo
{
    public string cancelType { get; set; }
}