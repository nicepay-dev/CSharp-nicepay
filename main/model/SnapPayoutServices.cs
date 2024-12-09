using CreateEwallet;
using Newtonsoft.Json;

namespace SignatureGenerator
{
    public class SnapPayoutServices
    {
        private string _clientId;

        public SnapPayoutServices(string clientId)
    {
        _clientId = clientId;
    }

        public string GeneratePayoutRequest(
            string merchantId, 
            string beneficiaryAccountNo, 
            string beneficiaryName, 
            string beneficiaryPhone,
            string payoutMethod, 
            string beneficiaryBankCode,  
            string partnerReferenceNo,
            string beneficiaryCustomerType,
            string beneficiaryCustomerResidence, 
            string reservedDt,
            string reservedTm,
            string value,
            string currency,
            string description)
        {
            var builder = new PayoutRequestBuilder(
            merchantId, 
            beneficiaryAccountNo, 
            beneficiaryName, 
            beneficiaryPhone, 
            payoutMethod, 
            beneficiaryBankCode, 
            partnerReferenceNo, 
            beneficiaryCustomerType, 
            beneficiaryCustomerResidence,
            reservedTm,
            reservedDt, 
            description)
            .SetAmount(value,currency);

            var requestBodyObj = builder.Build();
            return JsonConvert.SerializeObject(requestBodyObj);
        }

        public string ApprovePayoutRequest(string originalReferenceNo, 
            string originalPartnerReferenceNo, 
            string merchantId)
        {
        var builder = new PayoutApproveBuilder(originalPartnerReferenceNo, originalReferenceNo,merchantId);
           
            var requestBodyObj = builder.Build();
            return JsonConvert.SerializeObject(requestBodyObj);
        }


        public string RejectPayoutRequest(string originalReferenceNo, 
            string originalPartnerReferenceNo, 
            string merchantId)
        {
        var builder = new PayoutRejectBuilder(originalPartnerReferenceNo, originalReferenceNo,merchantId);
           
            var requestBodyObj = builder.Build();
            return JsonConvert.SerializeObject(requestBodyObj);
        }

        
        public string InquiryPayoutRequest(string originalReferenceNo, 
            string originalPartnerReferenceNo, 
            string merchantId,
            string beneficiaryAccountNo)
        {
        var builder = new PayoutInquiryBuilder(originalPartnerReferenceNo, originalReferenceNo,merchantId,beneficiaryAccountNo);
           
            var requestBodyObj = builder.Build();
            return JsonConvert.SerializeObject(requestBodyObj);
        }


        public string CancelPayoutRequest(string originalReferenceNo, 
            string originalPartnerReferenceNo, 
            string merchantId)
        {
        var builder = new PayoutCancelBuilder(originalPartnerReferenceNo, originalReferenceNo,merchantId);
           
            var requestBodyObj = builder.Build();
            return JsonConvert.SerializeObject(requestBodyObj);
        }       


        public string BalancePayoutRequest(string accountNo, string msId)
        {
        var builder = new PayoutBalanceBuilder(accountNo)
        .SetAdditionalInfo(msId);
           
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
