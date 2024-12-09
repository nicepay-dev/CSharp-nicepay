using CreateVA;
public class CreateVARequestBuilder
{
    private readonly virtualAccount _request;

    public CreateVARequestBuilder(string partnerServiceId)
    {
        _request = new virtualAccount
        {
            partnerServiceId = partnerServiceId
        };
    }

    public CreateVARequestBuilder SetCustomerNo(string customerNo)
    {
        _request.customerNo = customerNo;
        return this;
    }

    public CreateVARequestBuilder SetVirtualAccountName(string virtualAccountName)
    {
        _request.virtualAccountName = virtualAccountName;
        return this;
    }

    public CreateVARequestBuilder SetTrxId(string trxId)
    {
        _request.trxId = trxId;
        return this;
    }

    public CreateVARequestBuilder SetTotalAmount(string value, string currency)
    {
        _request.totalAmount = new TotalAmount
        {
            value = value,
            currency = currency
        };
        return this;
    }

    public CreateVARequestBuilder SetAdditionalInfo(string bankCd, string goodsNm, string dbProcessUrl)
    {
        _request.additionalInfo = new AdditionalInfo
        {
            bankCd = bankCd,
            goodsNm = goodsNm,
            dbProcessUrl = dbProcessUrl
        };
        return this;
    }

    public virtualAccount Build()
    {
        return _request;
    }
}