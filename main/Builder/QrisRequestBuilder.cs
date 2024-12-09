
using CreateQris;
using static CreateQris.qrisRequest;
public class QrisRequestBuilder
{
    private readonly qrisRequest _request;

        public QrisRequestBuilder(string partnerReferenceNo, string merchantId, string storeId, string validityPeriod)
    {
        _request = new qrisRequest
        {
            partnerReferenceNo = partnerReferenceNo,
            merchantId = merchantId,
            storeId = storeId,
            validityPeriod = validityPeriod
        };
    }

    public QrisRequestBuilder SetAmount(string value, string currency)
    {
        _request.amount = new Amount
        {
            value = value,
            currency = currency
        };
        return this;
    }

    public QrisRequestBuilder SetAdditionalInfo(string goodsNm, string billingNm, string billingPhone, string billingEmail, string billingCity, string billingState, string billingPostCd, string billingCountry, string callBackUrl, string dbProcessUrl, string mitraCd, string userIP, string cartData)
    {
        _request.additionalInfo = new AdditionalInfo
        {
            goodsNm = goodsNm,
            billingNm = billingNm,
            billingPhone = billingPhone,
            billingEmail = billingEmail,
            billingCity = billingCity,
            billingState = billingState,
            billingPostCd = billingPostCd,
            billingCountry = billingCountry,
            callBackUrl = callBackUrl,
            dbProcessUrl = dbProcessUrl,
            mitraCd = mitraCd,
            userIP = userIP,
            cartData = cartData
        };
        return this;
    }

    public qrisRequest Build()
    {
        return _request;
    }
}