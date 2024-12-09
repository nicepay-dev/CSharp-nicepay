
using InquiryQris;
using static InquiryQris.inquiryQris;
public class InquiryQrisBuilder
{
   
    private readonly inquiryQris _request;

    public InquiryQrisBuilder(string originalReferenceNo, string originalPartnerReferenceNo, string merchantId, string externalStoreId, string serviceCode)
    {
        _request = new inquiryQris
        {
            originalReferenceNo = originalReferenceNo,
            originalPartnerReferenceNo = originalPartnerReferenceNo,
            merchantId = merchantId,
            externalStoreId = externalStoreId,
            serviceCode = serviceCode,
            additionalInfo = new AdditionalInfo() // Empty additionalInfo by default
        };
    }

    public InquiryQrisBuilder SetAdditionalInfo(AdditionalInfo additionalInfo)
    {
        _request.additionalInfo = additionalInfo;
        return this;
    }

    public inquiryQris Build()
    {
        return _request;
    }
}
