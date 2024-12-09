
using CreateEwallet;
using static CreateEwallet.ewalletRequest;
public class EwalletRequestBuilder
{
    private readonly ewalletRequest _request;

        public EwalletRequestBuilder(string partnerReferenceNo, string merchantId)
        {
            _request = new ewalletRequest
            {
                partnerReferenceNo = partnerReferenceNo,
                merchantId = merchantId,
                urlParam = new List<UrlParam>()
            };
        }

        public EwalletRequestBuilder SetSubMerchantId(string subMerchantId)
        {
            _request.subMerchantId = subMerchantId;
            return this;
        }

        public EwalletRequestBuilder SetExternalStoreId(string externalStoreId)
        {
            _request.externalStoreId = externalStoreId;
            return this;
        }

        public EwalletRequestBuilder SetValidUpTo(string validUpTo)
        {
            _request.validUpTo = validUpTo;
            return this;
        }

        public EwalletRequestBuilder SetAmount(string value, string currency)
        {
            _request.amount = new Amount
            {
                value = value,
                currency = currency
            };
            return this;
        }

        public EwalletRequestBuilder AddUrlParam(string url, string type, string isDeeplink)
        {
            _request.urlParam.Add(new UrlParam
            {
                url = url,
                type = type,
                isDeeplink = isDeeplink
            });
            return this;
        }

        public EwalletRequestBuilder SetAdditionalInfo(
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
            _request.additionalInfo = new AdditionalInfo
            {
                mitraCd = mitraCd,
                goodsNm = goodsNm,
                billingNm = billingNm,
                billingPhone = billingPhone,
                cartData = cartData,
                dbProcessUrl = dbProcessUrl,
                callBackUrl = callBackUrl,
                msId = msId,
                msFee = msFee,
                msFeeType = msFeeType,
                mbFee = mbFee,
                mbFeeType = mbFeeType
            };
            return this;
        }

        public ewalletRequest Build()
        {
            return _request;
        }

}