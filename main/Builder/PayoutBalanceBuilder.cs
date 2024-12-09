
using CreatePayout;
using static CreatePayout.balanceInquiryPayout;
public class PayoutBalanceBuilder
{
    private readonly balanceInquiryPayout _request;

        public PayoutBalanceBuilder(string accountNo)
    {
        _request = new balanceInquiryPayout
        {
        accountNo = accountNo
      
        };
    }

     public PayoutBalanceBuilder SetAdditionalInfo(string msId)
    {
        _request.additionalInfo = new AdditionalInfo
        {
            msId = msId
        };
        return this;
    }

    public balanceInquiryPayout Build()
    {
        return _request;
    }
}