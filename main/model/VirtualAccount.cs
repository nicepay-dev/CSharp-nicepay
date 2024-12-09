
using System.Dynamic;

namespace CreateVA;

public class virtualAccount {
   public string partnerServiceId { get; set; }
    public string customerNo { get; set; }
    public string virtualAccountNo { get; set; } = "";
    public string? virtualAccountName { get; set; }
    public string trxId { get; set; }
    public TotalAmount totalAmount { get; set; }
    public AdditionalInfo additionalInfo { get; set; }

}


public class TotalAmount
{
    public string value { get; set; }
    public string currency { get; set; }
}

public class AdditionalInfo
{
    public string bankCd { get; set; }
    public string goodsNm { get; set; }
    public string dbProcessUrl { get; set; }
}
