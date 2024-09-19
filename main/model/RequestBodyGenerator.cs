using Newtonsoft.Json;

namespace SignatureGenerator
{
    public class RequestBodyGenerator
    {
        private string _clientId;

        public RequestBodyGenerator(string clientId)
    {
        _clientId = clientId;
    }

        public string GenerateCreateVARequest(string customerNo, string virtualAccountName, string trxId, string totalAmountValue, string currency, string bankCd, string goodsNm, string dbProcessUrl)
        {
            var requestBodyObj = new
            {
               partnerServiceId = _clientId,
               customerNo = customerNo,
               virtualAccountNo = "",
               virtualAccountName = virtualAccountName,
               trxId = trxId,
               totalAmount = new
            {
                value = totalAmountValue,
                currency = currency
            },
            additionalInfo = new
            {
                bankCd = bankCd,
                goodsNm = goodsNm,
                dbProcessUrl = dbProcessUrl
                }
            };

            return JsonConvert.SerializeObject(requestBodyObj);
        }

        public string GenerateInquiryRequest(string customerNo, string virtualAccountNo, string inquiryRequestId, string totalAmountValue, string currency, string trxId, string tXidVA)
        {
           var requestBodyObj = new
            {
            partnerServiceId = _clientId,
            customerNo = customerNo,
            virtualAccountNo = virtualAccountNo,
            inquiryRequestId = inquiryRequestId,
            additionalInfo = new
            {
                totalAmount = new
                {
                    value = totalAmountValue,
                    currency = currency
                },
                trxId = trxId,
                tXidVA = tXidVA
            }
        };

            return JsonConvert.SerializeObject(requestBodyObj);
        }

        public string GenerateDeleteVARequest(string virtualAccountNo, string trxId, string totalAmount, string tXidVA)
        {
            var requestBodyDel = new
        {
        partnerServiceId =  _clientId,
        customerNo =  "",
        virtualAccountNo =  virtualAccountNo,
        trxId = trxId,
        additionalInfo = new {
            totalAmount = new {
                value = totalAmount,
                currency = "IDR"
        },
        tXidVA = tXidVA,
        cancelMessage = "Cancel Virtual Account"
    }
};
            return JsonConvert.SerializeObject(requestBodyDel);
        }
    }
}
