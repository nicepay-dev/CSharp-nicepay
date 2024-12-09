
using System.Dynamic;
using Newtonsoft.Json;

namespace CreatePayout;

public class payoutRequest {
   public string merchantId { get; set; }
    public string msId { get; set; }
    public string beneficiaryAccountNo { get; set; }
    public string beneficiaryName { get; set; }
    public string beneficiaryPhone { get; set; }
    public string beneficiaryCustomerResidence { get; set; }
    public string beneficiaryCustomerType { get; set; }
    public string beneficiaryPostalCode { get; set; }
    public string payoutMethod { get; set; }
    public string beneficiaryBankCode { get; set; }
    public Amount amount { get; set; }
    public string partnerReferenceNo { get; set; }
    public string reservedDt { get; set; }
    public string reservedTm { get; set; }
    public string description { get; set; }
    public string deliveryName { get; set; }
    public string deliveryId { get; set; }
    public string beneficiaryPOE { get; set; }
    public string beneficiaryDOE { get; set; }
    public string beneficiaryCoNo { get; set; }
    public string beneficiaryAddress { get; set; }
    public string beneficiaryAuthPhoneNumber { get; set; }
    public string beneficiaryMerCategory { get; set; }
    public string beneficiaryCoMgmtName { get; set; }
    public string beneficiaryCoShName { get; set; }
}

public class Amount
{
    public string value { get; set; }
    public string currency { get; set; }

}

public class generalPayout {
     public string merchantId { get; set; }
    public string originalPartnerReferenceNo { get; set; }
    public string originalReferenceNo { get; set; }
}


public class balanceInquiryPayout {
     public string accountNo { get; set; }
    public AdditionalInfo additionalInfo { get; set; }
    
}

public class AdditionalInfo
{
    public string msId { get; set; }
    
}


public class inquiryPayout {
     public string merchantId { get; set; }
    public string originalPartnerReferenceNo { get; set; }
    public string originalReferenceNo { get; set; }
    public string beneficiaryAccountNo { get; set; }
}
