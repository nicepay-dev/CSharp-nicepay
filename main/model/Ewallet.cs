
using System.Dynamic;
using Newtonsoft.Json;

namespace CreateEwallet;

public class ewalletRequest {
   public string partnerReferenceNo { get; set; }
    public string merchantId { get; set; }
    public string subMerchantId { get; set; } = "";
    public string? externalStoreId { get; set; }
    public string validUpTo { get; set; }
    public Amount amount { get; set; }
    public List<UrlParam> urlParam { get; set; }
    public AdditionalInfo additionalInfo { get; set; }

}


public class Amount
{
    public string value { get; set; }
    public string currency { get; set; }
}

public class UrlParam
    {
        public string url { get; set; }
        public string type { get; set; }
        public string isDeeplink { get; set; }
    }

    public class AdditionalInfo
    {
        public string mitraCd { get; set; }
        public string goodsNm { get; set; }
        public string billingNm { get; set; }
        public string billingPhone { get; set; }
        public string cartData { get; set; }
        public string dbProcessUrl { get; set; }
        public string callBackUrl { get; set; }
        public string msId { get; set; }
        public string msFee { get; set; }
        public string msFeeType { get; set; }
        public string mbFee { get; set; }
        public string mbFeeType { get; set; }
    }

    public class CartData
{
    public string count { get; set; }
    public List<CartItem> item { get; set; }
}

public class CartItem
{
    public string img_url { get; set; }
    public string goods_name { get; set; }
    public string goods_detail { get; set; }
    public string goods_amt { get; set; }
    public string goods_quantity { get; set; }
}


