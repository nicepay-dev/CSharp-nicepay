using Newtonsoft.Json;

namespace SignatureGenerator
{
    public class SnapQrisServices
    {
        private string _clientId;

        public SnapQrisServices(string clientId)
    {
        _clientId = clientId;
    }
        
public string GenerateQrisRequest(
    string partnerReferenceNo, 
    string merchantId, 
    string storeId, 
    string validityPeriod, 
    string amountValue, 
    string currency, 
    string goodsNm, 
    string billingNm, 
    string billingPhone, 
    string billingEmail, 
    string billingCity, 
    string billingState, 
    string billingPostCd, 
    string billingCountry, 
    string callBackUrl, 
    string dbProcessUrl, 
    string mitraCd, 
    string userIP, 
    string cartData)
{
    var builder = new QrisRequestBuilder(partnerReferenceNo, merchantId, storeId, validityPeriod)
        .SetAmount(amountValue, currency)
        .SetAdditionalInfo(
            goodsNm,
            billingNm,
            billingPhone,
            billingEmail,
            billingCity,
            billingState,
            billingPostCd,
            billingCountry,
            callBackUrl,
            dbProcessUrl,
            mitraCd,
            userIP,
            cartData);

    var requestBodyObj = builder.Build();
    return JsonConvert.SerializeObject(requestBodyObj);
}

 public string GenerateInquiryQris(string originalReferenceNo,
        string originalPartnerReferenceNo,
        string merchantId,
        string externalStoreId,
        string serviceCode)
        {
        var builder = new InquiryQrisBuilder( originalReferenceNo, originalPartnerReferenceNo, merchantId, externalStoreId, serviceCode);

            var requestBodyObj = builder.Build();
            return JsonConvert.SerializeObject(requestBodyObj);
        }

 public string GenerateRefundQris(string originalReferenceNo,
        string originalPartnerReferenceNo,
        string merchantId,
        string partnerRefundNo,
        string externalStoreId,
        string value,
        string reason,
        string cancelType,
        string currency)
        {
        var builder = new RefundQrisBuilder( originalReferenceNo, originalPartnerReferenceNo, partnerRefundNo, merchantId)
        .SetExternalStoreId(externalStoreId)
        .SetRefundAmount(value, currency)
        .SetReason(reason)
        .SetAdditionalInfo(cancelType);

            var requestBodyObj = builder.Build();
            return JsonConvert.SerializeObject(requestBodyObj);
        }


    }
}
