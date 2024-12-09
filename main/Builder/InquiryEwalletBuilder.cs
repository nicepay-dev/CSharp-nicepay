
using InquiryEwallet;
using static InquiryEwallet.inquiryEwallet;
public class InquiryEwalletBuilder
{
    private readonly inquiryEwallet _request;

    public InquiryEwalletBuilder(string merchantId, string subMerchantId, string originalPartnerReferenceNo, string originalReferenceNo, string serviceCode)
    {
        _request = new inquiryEwallet
        {
            merchantId = merchantId,
            subMerchantId = subMerchantId,
            originalPartnerReferenceNo = originalPartnerReferenceNo,
            originalReferenceNo = originalReferenceNo,
            serviceCode = serviceCode,
            additionalInfo = new AdditionalInfo()  // Empty additionalInfo by default
        };
    }

    public InquiryEwalletBuilder SetTransactionDate(DateTime transactionDate)
    {
        _request.transactionDate = transactionDate.ToString("yyyy-MM-ddTHH:mm:sszzz");
        return this;
    }

    public InquiryEwalletBuilder SetExternalStoreId(string externalStoreId)
    {
        _request.externalStoreId = externalStoreId;
        return this;
    }

    public InquiryEwalletBuilder SetAmount(string value, string currency)
    {
        _request.amount = new Amount
        {
            value = value,
            currency = currency
        };
        return this;
    }

    public inquiryEwallet Build()
    {
        return _request;
    }
}
