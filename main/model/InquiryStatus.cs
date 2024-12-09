
using System.Dynamic;

namespace InquiryStatus;


//VA
public class inquiryRequest
{
    public string partnerServiceId { get; set; }
    public string customerNo { get; set; }
    public string virtualAccountNo { get; set; }
    public string inquiryRequestId { get; set; }
    public AdditionalInfo additionalInfo { get; set; }

    public class AdditionalInfo
    {
     public TotalAmount totalAmount { get; set; }
    public string trxId { get; set; }
    public string tXidVA { get; set; }
    }
    public class TotalAmount
    {
        public string value { get; set; }
        public string currency { get; set; }
    }

}



