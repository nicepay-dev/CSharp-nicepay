
using RefundQris;
using static RefundQris.refundRequest;
public class RefundQrisBuilder
{
    private readonly refundRequest _request;

    public RefundQrisBuilder(string originalReferenceNo, string originalPartnerReferenceNo, string partnerRefundNo, string merchantId)
    {
        _request = new refundRequest
        {
            originalReferenceNo = originalReferenceNo,
            originalPartnerReferenceNo = originalPartnerReferenceNo,
            partnerRefundNo = partnerRefundNo,
            merchantId = merchantId,
            additionalInfo = new AdditionalInfo() // Default empty additional info
        };
    }

    public RefundQrisBuilder SetExternalStoreId(string externalStoreId)
    {
        _request.externalStoreId = externalStoreId;
        return this;
    }

    public RefundQrisBuilder SetRefundAmount(string value, string currency)
    {
        _request.refundAmount = new Amount
        {
            value = value,
            currency = currency
        };
        return this;
    }

    public RefundQrisBuilder SetReason(string reason)
    {
        _request.reason = reason;
        return this;
    }

    public RefundQrisBuilder SetAdditionalInfo(string cancelType)
    {
        _request.additionalInfo.cancelType = cancelType;
        return this;
    }

    public refundRequest Build()
    {
        return _request;
    }
}