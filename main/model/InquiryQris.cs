
using System.Dynamic;

namespace InquiryQris;


public class inquiryQris
{
   public string originalReferenceNo { get; set; }
        public string originalPartnerReferenceNo { get; set; }
        public string merchantId { get; set; }
        public string externalStoreId { get; set; }
        public string serviceCode { get; set; }
        public AdditionalInfo additionalInfo { get; set; }
}

public class AdditionalInfo
{
    // Add fields if additionalInfo will contain specific properties
}




