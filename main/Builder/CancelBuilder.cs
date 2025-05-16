public class CancelBuilder
{
    private Dictionary<string, object> _requestBody = new Dictionary<string, object>();

    public CancelBuilder SetCancelVASnap(
        string partnerServiceId,
        string customerNo,
        string virtualAccountNo,
        string trxId,
        string amountValue,
        string currency,
        string tXidVA,
        string cancelMessage)
    {
        _requestBody["partnerServiceId"] = partnerServiceId;
        _requestBody["customerNo"] = customerNo;
        _requestBody["virtualAccountNo"] = virtualAccountNo;
        _requestBody["trxId"] = trxId;

        _requestBody["additionalInfo"] = new Dictionary<string, object>
        {
            ["totalAmount"] = new Dictionary<string, object>
            {
                ["value"] = amountValue,
                ["currency"] = currency
            },
            ["tXidVA"] = tXidVA,
            ["cancelMessage"] = cancelMessage
        };

        return this;
    }

    // Versi dari object Cancel Qris SNAP
    public CancelBuilder SetRefundQrisSnap(
        string originalReferenceNo,
        string originalPartnerReferenceNo,
        string partnerRefundNo,
        string merchantId,
        string externalStoreId,
        string refundValue,
        string refundCurrency,
        string reason,
        string cancelType)
    {
        _requestBody["originalReferenceNo"] = originalReferenceNo;
        _requestBody["originalPartnerReferenceNo"] = originalPartnerReferenceNo;
        _requestBody["partnerRefundNo"] = partnerRefundNo;
        _requestBody["merchantId"] = merchantId;
        _requestBody["externalStoreId"] = externalStoreId;

        _requestBody["refundAmount"] = new Dictionary<string, object>
        {
            ["value"] = refundValue,
            ["currency"] = refundCurrency
        };

        _requestBody["reason"] = reason;

        _requestBody["additionalInfo"] = new Dictionary<string, object>
        {
            ["cancelType"] = cancelType
        };

        return this;
    }

 // Versi dari object Cancel Ewallet SNAP
    public CancelBuilder SetRefundEwalletSnap(
        string merchantId,
        string subMerchantId,
        string originalPartnerReferenceNo,
        string originalReferenceNo,
        string partnerRefundNo,
        string refundValue,
        string refundCurrency,
        string externalStoreId,
        string reason,
        string refundType)
    {
        _requestBody["merchantId"] = merchantId;
        _requestBody["subMerchantId"] = subMerchantId;
        _requestBody["originalPartnerReferenceNo"] = originalPartnerReferenceNo;
        _requestBody["originalReferenceNo"] = originalReferenceNo;
        _requestBody["partnerRefundNo"] = partnerRefundNo;
        _requestBody["externalStoreId"] = externalStoreId;
        _requestBody["reason"] = reason;

        _requestBody["refundAmount"] = new Dictionary<string, object>
        {
            ["value"] = refundValue,
            ["currency"] = refundCurrency
        };

        _requestBody["additionalInfo"] = new Dictionary<string, object>
        {
            ["refundType"] = refundType
        };

        return this;
    }

    public Dictionary<string, object> Build()
    {
        return _requestBody;
    }
}