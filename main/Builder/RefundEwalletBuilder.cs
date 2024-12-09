
using RefundEwallet;
using static RefundEwallet.refundRequest;
public class RefundEwalletBuilder
{
    private readonly refundRequest _refundRequest;

    public RefundEwalletBuilder(string merchantId, string subMerchantId)
    {
        _refundRequest = new refundRequest
        {
            merchantId = merchantId,
            subMerchantId = subMerchantId
        };
    }

    public RefundEwalletBuilder SetOriginalPartnerReferenceNo(string originalPartnerReferenceNo)
    {
        _refundRequest.originalPartnerReferenceNo = originalPartnerReferenceNo;
        return this;
    }

    public RefundEwalletBuilder SetOriginalReferenceNo(string originalReferenceNo)
    {
        _refundRequest.originalReferenceNo = originalReferenceNo;
        return this;
    }

    public RefundEwalletBuilder SetPartnerRefundNo(string partnerRefundNo)
    {
        _refundRequest.partnerRefundNo = partnerRefundNo;
        return this;
    }

    public RefundEwalletBuilder SetRefundAmount(string value, string currency)
    {
        _refundRequest.refundAmount = new RefundAmount
        {
            value = value,
            currency = currency
        };
        return this;
    }

    public RefundEwalletBuilder SetExternalStoreId(string externalStoreId)
    {
        _refundRequest.externalStoreId = externalStoreId;
        return this;
    }

    public RefundEwalletBuilder SetReason(string reason)
    {
        _refundRequest.reason = reason;
        return this;
    }

    public RefundEwalletBuilder SetAdditionalInfo(string refundType)
    {
        _refundRequest.additionalInfo = new AdditionalInfo
        {
            refundType = refundType
        };
        return this;
    }

    public refundRequest Build()
    {
        return _refundRequest;
    }
}