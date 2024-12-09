
using InquiryStatus;
using static InquiryStatus.inquiryRequest;
public class InquiryStatusBuilder
{
    private readonly inquiryRequest _request;

     public InquiryStatusBuilder(string partnerServiceId)
    {
        _request = new inquiryRequest
        {
            partnerServiceId = partnerServiceId
        };
    }

    public InquiryStatusBuilder SetCustomerNo(string customerNo)
    {
        _request.customerNo = customerNo;
        return this;
    }

    public InquiryStatusBuilder SetVirtualAccountNo(string virtualAccountNo)
    {
        _request.virtualAccountNo = virtualAccountNo;
        return this;
    }

    public InquiryStatusBuilder SetInquiryRequestId(string inquiryRequestId)
    {
        _request.inquiryRequestId = inquiryRequestId;
        return this;
    }

    public InquiryStatusBuilder SetAdditionalInfo(string value, string currency, string trxId, string tXidVA)
    {
        _request.additionalInfo = new AdditionalInfo
        {
            totalAmount = new TotalAmount
            {
                value = value,
                currency = currency
            },
            trxId = trxId,
            tXidVA = tXidVA
        };
        return this;
    }

    public inquiryRequest Build()
    {
        return _request;
    }

}