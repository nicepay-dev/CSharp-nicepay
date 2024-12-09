
using CreatePayout;
using static CreatePayout.payoutRequest;
public class PayoutRequestBuilder
{
    private readonly payoutRequest _request;

        public PayoutRequestBuilder(
            string merchantId, 
            string beneficiaryAccountNo, 
            string beneficiaryName, 
            string beneficiaryPhone,
            string payoutMethod, 
            string beneficiaryBankCode,  
            string partnerReferenceNo,
            string beneficiaryCustomerType,
            string beneficiaryCustomerResidence, 
            string reservedTm,
            string reservedDt ,
            string description)
    {
        _request = new payoutRequest
        {
        merchantId = merchantId,
        beneficiaryAccountNo = beneficiaryAccountNo,
        beneficiaryName = beneficiaryName,
        beneficiaryPhone =beneficiaryPhone,
        beneficiaryCustomerResidence = beneficiaryCustomerResidence,
        beneficiaryCustomerType =beneficiaryCustomerType,
        payoutMethod = payoutMethod,
        beneficiaryBankCode = beneficiaryBankCode,
        partnerReferenceNo = partnerReferenceNo,
        reservedDt  = reservedDt,
        reservedTm = reservedTm,
        description = description
        };
    }

    public PayoutRequestBuilder SetAmount(string value, string currency)
    {
        _request.amount = new Amount
        {
        value = value,
        currency = currency
        };
        return this;
    }
   

    public payoutRequest Build()
    {
        return _request;
    }
}