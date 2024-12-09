using Newtonsoft.Json;

namespace SignatureGenerator
{
    public class SnapEwalletServices
    {
        private string _clientId;

        public SnapEwalletServices(string clientId)
    {
        _clientId = clientId;
    }

       public string GenerateEwalletRequest(
    string partnerReferenceNo,
    string merchantId,
    string externalStoreId,
    string value,
    string currency,
    string mitraCd,
    string goodsNm,
    string billingNm,
    string billingPhone,
    string cartData,
    string dbProcessUrl,
    string callBackUrl,
    string msId,
    string msFee,
    string msFeeType,
    string mbFee,
    string mbFeeType)
{
    var builder = new EwalletRequestBuilder(partnerReferenceNo, merchantId)
        .SetExternalStoreId(externalStoreId)
        .SetAmount(value, currency)
        .AddUrlParam("https://test2.bi.go.id/v1/test", "PAY_NOTIFY", "Y")
        .AddUrlParam("https://test2.bi.go.id/v1/test", "PAY_RETURN", "Y")
        .SetAdditionalInfo(mitraCd, goodsNm, billingNm, billingPhone, cartData, dbProcessUrl, callBackUrl, msId, msFee, msFeeType, mbFee, mbFeeType);

    var requestBodyObj = builder.Build();
    return JsonConvert.SerializeObject(requestBodyObj);
}

public string GenerateStatusEwalletRequest(
    string merchantId,
    string subMerchantId,
    string originalPartnerReferenceNo,
    string originalReferenceNo,
    string serviceCode,
    DateTime transactionDate,
    string externalStoreId,
    string amountValue,
    string currency)
{
    var builder = new InquiryEwalletBuilder(merchantId, subMerchantId, originalPartnerReferenceNo, originalReferenceNo, serviceCode)
        .SetTransactionDate(transactionDate)
        .SetExternalStoreId(externalStoreId)
        .SetAmount(amountValue, currency);

    var requestBodyObj = builder.Build();
    return JsonConvert.SerializeObject(requestBodyObj);
}


public string GenerateRefundEwalletRequest(string merchantId, string subMerchantId, string originalPartnerReferenceNo, string originalReferenceNo, string partnerRefundNo, string refundAmountValue, string currency, string externalStoreId, string reason, string refundType)
{
    var builder = new RefundEwalletBuilder(merchantId, subMerchantId)
        .SetOriginalPartnerReferenceNo(originalPartnerReferenceNo)
        .SetOriginalReferenceNo(originalReferenceNo)
        .SetPartnerRefundNo(partnerRefundNo)
        .SetRefundAmount(refundAmountValue, currency)
        .SetExternalStoreId(externalStoreId)
        .SetReason(reason)
        .SetAdditionalInfo(refundType);

    var requestBodyObj = builder.Build();
    return JsonConvert.SerializeObject(requestBodyObj);
}


    }
}
