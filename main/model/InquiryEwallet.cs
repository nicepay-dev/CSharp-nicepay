
using System.Dynamic;

namespace InquiryEwallet;


public class inquiryEwallet
{
    public string merchantId { get; set; }
    public string subMerchantId { get; set; }
    public string originalPartnerReferenceNo { get; set; }
    public string originalReferenceNo { get; set; }
    public string serviceCode { get; set; }
    public string transactionDate { get; set; }
    public string externalStoreId { get; set; }
    public Amount amount { get; set; }
    public AdditionalInfo additionalInfo { get; set; }
}

public class Amount
{
    public string value { get; set; }
    public string currency { get; set; }
}

public class AdditionalInfo
{
    // Add fields if additionalInfo will contain specific properties
}




