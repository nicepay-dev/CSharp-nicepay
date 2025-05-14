public class InquiryBuilder
{
    private Dictionary<string, object> _requestBody = new Dictionary<string, object>();

    public InquiryBuilder SetInquiryVaSnap(
    string partnerServiceId, 
    string customerNo, 
    string virtualAccountNo, 
    string inquiryRequestId, 
    string value, 
    string currency, 
    string trxId, 
    string tXidVA)
    {
        _requestBody["partnerServiceId"] = partnerServiceId;
        _requestBody["customerNo"] = customerNo;
        _requestBody["virtualAccountNo"] = virtualAccountNo;
        _requestBody["inquiryRequestId"] = inquiryRequestId; 
        _requestBody["additionalInfo"] = new Dictionary<string, object>
        {
            ["totalAmount"] = new Dictionary<string, string>
            {
                ["value"] = value,
                ["currency"] = currency
            },
            ["trxId"] = trxId,
            ["tXidVA"] = tXidVA
        };
        
        return this;
    }

    // Versi dari object Inquiry Qris SNAP
    public InquiryBuilder SetInquiryQrisSnap(
        string originalReferenceNo, 
        string originalPartnerReferenceNo, 
        string merchantId, 
        string externalStoreId, 
        string serviceCode)
    {
        _requestBody["originalReferenceNo"] = originalReferenceNo;
        _requestBody["merchantId"] = merchantId;
        _requestBody["originalPartnerReferenceNo"] = originalPartnerReferenceNo;
        _requestBody["externalStoreId"] = externalStoreId;
        _requestBody["serviceCode"] = serviceCode;
        _requestBody["additionalInfo"] = new Dictionary<string, object>();

        return this;
    }


    public InquiryBuilder SetInquiryEwalletSnap(
        string merchantId,
        string subMerchantId,
        string originalPartnerReferenceNo,
        string originalReferenceNo,
        string serviceCode,
        string transactionDate,
        string externalStoreId,
        string amountValue,
        string currency)
    {
        _requestBody["merchantId"] = merchantId;
        _requestBody["subMerchantId"] = subMerchantId;
        _requestBody["originalPartnerReferenceNo"] = originalPartnerReferenceNo;
        _requestBody["originalReferenceNo"] = originalReferenceNo;
        _requestBody["serviceCode"] = serviceCode;
        _requestBody["transactionDate"] = transactionDate;
        _requestBody["externalStoreId"] = externalStoreId;

        _requestBody["amount"] = new Dictionary<string, object>
        {
            ["value"] = amountValue,
            ["currency"] = currency
        };

        _requestBody["additionalInfo"] = new Dictionary<string, object>(); 

        return this;
    }

    public InquiryBuilder SetInquiryPayoutSnap(
        string merchantId,
        string originalPartnerReferenceNo,
        string originalReferenceNo,
        string beneficiaryAccountNo)
    {
        _requestBody["merchantId"] = merchantId;
        _requestBody["originalPartnerReferenceNo"] = originalPartnerReferenceNo;
        _requestBody["originalReferenceNo"] = originalReferenceNo;
        _requestBody["beneficiaryAccountNo"] = beneficiaryAccountNo;

        return this;
    }

    public Dictionary<string, object> Build()
    {
        return _requestBody;
    }
}