
using CreatePayout;
using static CreatePayout.generalPayout;
public class PayoutCancelBuilder
{
    private readonly generalPayout _request;

        public PayoutCancelBuilder(
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