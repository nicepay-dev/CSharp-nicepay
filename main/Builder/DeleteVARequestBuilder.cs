
using DeleteVA;
using static DeleteVA.deleteVARequest;
public class DeleteVARequestBuilder
{
    private readonly deleteVARequest _request;

    public DeleteVARequestBuilder(string partnerServiceId)
    {
        _request = new deleteVARequest
        {
            partnerServiceId = partnerServiceId
        };
    }

    public DeleteVARequestBuilder SetVirtualAccountNo(string virtualAccountNo)
    {
        _request.virtualAccountNo = virtualAccountNo;
        return this;
    }

    public DeleteVARequestBuilder SetCustomerNo(string customerNo)
    {
        _request.customerNo = customerNo;
        return this;
    }

    public DeleteVARequestBuilder SetTrxId(string trxId)
    {
        _request.trxId = trxId;
        return this;
    }

    public DeleteVARequestBuilder SetAdditionalInfo(string value, string currency, string tXidVA, string cancelMessage)
    {
        _request.additionalInfo = new AdditionalInfo
        {
            totalAmount = new TotalAmount
            {
                value = value,
                currency = currency
            },
            tXidVA = tXidVA,
            cancelMessage = cancelMessage
        };
        return this;
    }

    public deleteVARequest Build()
    {
        return _request;
    }


}
