public class VirtualAccountBuilder
{
    private Dictionary<string, object> _requestBody = new Dictionary<string, object>();


        
    public VirtualAccountBuilder SetVirtualAccount(
    string iMid, 
    string timeStamp, 
    string payMethod, 
    string currency, 
    string amt, 
    string referenceNo, 
    string goodsNm, 
    string merchantToken,
    string bankCd,
    string billingNm,
    string billingPhone, 
    string billingEmail, 
    string billingAddr, 
    string billingCity, 
    string billingState,
    string billingPostCd, 
    string billingCountry, 
    string vacctValidDt,
    string vacctValidTm, 
    string dbProcessUrl,
    string merFixAcctId)
    {
        _requestBody["timeStamp"] = timeStamp;
        _requestBody["iMid"] = iMid;
        _requestBody["payMethod"] = payMethod;
        _requestBody["currency"] = currency;
        _requestBody["amt"] = amt;
        _requestBody["referenceNo"] = referenceNo;
        _requestBody["goodsNm"] = goodsNm;
        _requestBody["merchantToken"] = merchantToken;  // Merchant token included in body
        _requestBody["bankCd"] = bankCd;
        _requestBody["billingNm"] = billingNm;
        _requestBody["billingPhone"] = billingPhone;
        _requestBody["billingEmail"] = billingEmail;
        _requestBody["billingAddr"] = billingAddr;
        _requestBody["billingCity"] = billingCity;
        _requestBody["billingState"] = billingState;
        _requestBody["billingPostCd"] = billingPostCd;
        _requestBody["billingCountry"] = billingCountry;
        _requestBody["vacctValidDt"] = vacctValidDt;
        _requestBody["vacctValidTm"] = vacctValidTm;
        _requestBody["dbProcessUrl"] = dbProcessUrl;
        _requestBody["merFixAcctId"] = merFixAcctId;
        return this;
    }

    // Versi dari object virtualAccount SNAP
    public VirtualAccountBuilder SetVirtualAccountSnap(
        string partnerServiceId, 
        string customerNo, 
        string virtualAccountName, 
        string trxId,
        string value, 
        string currency, 
        string bankCd, 
        string goodsNm, 
        string dbProcessUrl)
    {
        _requestBody["partnerServiceId"] = partnerServiceId;
        _requestBody["customerNo"] = customerNo;
        _requestBody["virtualAccountNo"] = ""; // kosong karena akan diisi oleh sistem
        _requestBody["virtualAccountName"] = virtualAccountName;
        _requestBody["trxId"] = trxId;

        _requestBody["totalAmount"] = new Dictionary<string, object>
        {
            ["value"] = value,
            ["currency"] = currency
        };

        _requestBody["additionalInfo"] = new Dictionary<string, object>
        {
            ["bankCd"] = bankCd,
            ["goodsNm"] = goodsNm,
            ["dbProcessUrl"] = dbProcessUrl
        };

        return this;
    }

    public Dictionary<string, object> Build()
    {
        return _requestBody;
    }
}