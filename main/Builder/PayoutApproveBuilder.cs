
using CreatePayout;
using static CreatePayout.generalPayout;
public class PayoutApproveBuilder
{
    private readonly generalPayout _request;

        public PayoutApproveBuilder(
            string originalReferenceNo, 
            string originalPartnerReferenceNo, 
            string merchantId
            )
    {
        _request = new generalPayout
        {
        merchantId = merchantId,
        originalReferenceNo = originalReferenceNo,
        originalPartnerReferenceNo =originalPartnerReferenceNo
        };
    }
    public generalPayout Build()
    {
        return _request;
    }
}