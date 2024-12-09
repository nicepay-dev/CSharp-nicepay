
using System.Dynamic;

namespace RefundEwallet;


public class refundRequest
{
    public string merchantId { get; set; }
    public string subMerchantId { get; set; }
    public string originalPartnerReferenceNo { get; set; }
    public string originalReferenceNo { get; set; }
    public string partnerRefundNo { get; set; }
    public RefundAmount refundAmount { get; set; }
    public string externalStoreId { get; set; }
    public string reason { get; set; }
    public AdditionalInfo additionalInfo { get; set; }
}

public class RefundAmount
{
    public string value { get; set; }
    public string currency { get; set; }
}

public class AdditionalInfo
{
    public string refundType { get; set; }
}