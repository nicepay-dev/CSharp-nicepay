using Newtonsoft.Json;

namespace SignatureGenerator
{
    public class SnapVaServices
    {
        private string _clientId;

        public SnapVaServices(string clientId)
    {
        _clientId = clientId;
    }

        public string GenerateCreateVARequest(string customerNo, string virtualAccountName, string trxId, string totalAmountValue, string currency, string bankCd, string goodsNm, string dbProcessUrl)
        {
            var builder = new CreateVARequestBuilder(_clientId)
            .SetCustomerNo(customerNo)
            .SetVirtualAccountName(virtualAccountName)
            .SetTrxId(trxId)
            .SetTotalAmount(totalAmountValue, currency)
            .SetAdditionalInfo(bankCd, goodsNm, dbProcessUrl);

            var requestBodyObj = builder.Build();
            return JsonConvert.SerializeObject(requestBodyObj);
        }

        public string GenerateInquiryRequest(string customerNo, string virtualAccountNo, string inquiryRequestId, string value, string currency, string trxId, string tXidVA)
        {
        var builder = new InquiryStatusBuilder(_clientId)
            .SetCustomerNo(customerNo)
            .SetVirtualAccountNo(virtualAccountNo)
            .SetInquiryRequestId(inquiryRequestId)
            .SetAdditionalInfo(value, currency, trxId, tXidVA);

            var requestBodyObj = builder.Build();
            return JsonConvert.SerializeObject(requestBodyObj);
        }

        public string GenerateDeleteVARequest(string virtualAccountNo, string customerNo, string trxId, string value, string currency, string tXidVA, string cancelMessage)
        {  
             var builder = new DeleteVARequestBuilder(_clientId)
            .SetVirtualAccountNo(virtualAccountNo)
            .SetTrxId(trxId)
            .SetCustomerNo(customerNo)
            .SetAdditionalInfo(value, currency, tXidVA, cancelMessage);
            
            var requestBodyDel = builder.Build();
            return JsonConvert.SerializeObject(requestBodyDel);
        }
    }
}
