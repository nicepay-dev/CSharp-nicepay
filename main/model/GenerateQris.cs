
using System.Dynamic;
using Newtonsoft.Json;

namespace CreateQris;

public class qrisRequest {
    public string partnerReferenceNo { get; set; }
    public Amount amount { get; set; }
    public string merchantId { get; set; }
    public string storeId { get; set; }
    public string validityPeriod { get; set; }
    public AdditionalInfo additionalInfo { get; set; }
}

public class Amount
{
    public string value { get; set; }
    public string currency { get; set; }
}

public class AdditionalInfo
{
    public string goodsNm { get; set; }
    public string billingNm { get; set; }
    public string billingPhone { get; set; }
    public string billingEmail { get; set; }
    public string billingCity { get; set; }
    public string billingState { get; set; }
    public string billingPostCd { get; set; }
    public string billingCountry { get; set; }
    public string callBackUrl { get; set; }
    public string dbProcessUrl { get; set; }
    public string mitraCd { get; set; }
    public string userIP { get; set; }
    public string cartData { get; set; }
}


