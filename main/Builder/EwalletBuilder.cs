public class EwalletBuilder
{
    private Dictionary<string, object> _requestBody = new Dictionary<string, object>();


        
    public EwalletBuilder SetEwalletV2(
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
    string deliveryNm, 
    string deliveryPhone, 
    string deliveryAddr,
    string deliveryCity, 
    string deliveryState, 
    string deliveryPostCd, 
    string deliveryCountry,
    string dbProcessUrl, 
    string vat, 
    string fee, 
    string notaxAmt, 
    string description, 
    string reqDt, 
    string reqTm, 
    string reqDomain, 
    string reqServerIP, 
    string reqClientVer,
    string userIP, 
    string userSessionID, 
    string userAgent,
    string userLanguage, 
    string cartData, 
    string mitraCd)
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
        _requestBody["dbProcessUrl"] = dbProcessUrl;
        _requestBody["deliveryNm"] = deliveryNm;
        _requestBody["deliveryPhone"] = deliveryPhone;
        _requestBody["deliveryAddr"] = deliveryAddr;
        _requestBody["deliveryCity"] = deliveryCity;
        _requestBody["deliveryState"] = deliveryState;
        _requestBody["deliveryPostCd"] = deliveryPostCd;
        _requestBody["deliveryCountry"] = deliveryCountry;
        _requestBody["vat"] = vat;
        _requestBody["fee"] = fee;
        _requestBody["notaxAmt"] = notaxAmt;
        _requestBody["description"] = description;
        _requestBody["reqDt"] = reqDt;
        _requestBody["reqTm"] = reqTm;
        _requestBody["reqDomain"] = reqDomain;
        _requestBody["reqServerIP"] = reqServerIP;
        _requestBody["reqClientVer"] = reqClientVer;
        _requestBody["userIP"] = userIP;
        _requestBody["userSessionID"] = userSessionID;
        _requestBody["userAgent"] = userAgent;
        _requestBody["userLanguage"] = userLanguage;
        _requestBody["cartData"] = cartData;
        _requestBody["mitraCd"] = mitraCd;
        return this;
    }

    // Versi dari object Ewallet SNAP
    public EwalletBuilder SetEwalletSnap(
        string partnerReferenceNo,
        string merchantId,
        string subMerchantId,
        string validUpTo,
        object urlParam,
        string externalStoreId,
        string value,
        string currency,
        string mitraCd,
        string goodsNm,
        string billingNm,
        string billingPhone,
        string cartData,
        string dbProcessUrl,
        string callBackUrl,
        string msId,
        string msFee,
        string msFeeType,
        string mbFee,
        string mbFeeType)
    {
        _requestBody["partnerReferenceNo"] = partnerReferenceNo;
        _requestBody["merchantId"] = merchantId;
        _requestBody["subMerchantId"] = subMerchantId;
        _requestBody["externalStoreId"] = externalStoreId;
        _requestBody["validUpTo"] = validUpTo;

        _requestBody["amount"] = new Dictionary<string, object>
        {
            ["value"] = value,
            ["currency"] = currency
        };

        _requestBody["urlParam"] = urlParam;

        _requestBody["additionalInfo"] = new Dictionary<string, object>
        {
            ["mitraCd"] = mitraCd,
            ["goodsNm"] = goodsNm,
            ["billingNm"] = billingNm,
            ["billingPhone"] = billingPhone,
            ["cartData"] = cartData,
            ["dbProcessUrl"] = dbProcessUrl,
            ["callBackUrl"] = callBackUrl,
            ["msId"] = msId,
            ["msFee"] = msFee,
            ["msFeeType"] = msFeeType,
            ["mbFee"] = mbFee,
            ["mbFeeType"] = mbFeeType
        };

        return this;
    }


    public Dictionary<string, object> Build()
    {
        return _requestBody;
    }
}