
using CreatePayout;
using static CreatePayout.inquiryPayout;
public class PayoutInquiryBuilder
{
    private readonly inquiryPayout _request;

        public PayoutInquiryBuilder(
            string originalReferenceNo, 
            string originalPartnerReferenceNo, 
            string merchantId,
            string beneficiaryAccountNo
            )
    {
        _request = new inquiryPayout
        {
        merchantId = merchantId,
        originalReferenceNo = originalReferenceNo,
        originalPartnerReferenceNo =originalPartnerReferenceNo,
        beneficiaryAccountNo = beneficiaryAccountNo
        };
    }
    public inquiryPayout Build()
    {
        return _request;
    }
}