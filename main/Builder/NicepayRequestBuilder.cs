public class NicepayRequestBuilder
{
    private Dictionary<string, object> _requestBody = new Dictionary<string, object>();

    // Set Common Fields
    public NicepayRequestBuilder SetCommonFields(string iMid, string timeStamp, string payMethod, string currency, string amt, string referenceNo, string goodsNm, string merchantToken)
    {
        _requestBody["timeStamp"] = timeStamp;
        _requestBody["iMid"] = iMid;
        _requestBody["payMethod"] = payMethod;
        _requestBody["currency"] = currency;
        _requestBody["amt"] = amt;
        _requestBody["referenceNo"] = referenceNo;
        _requestBody["goodsNm"] = goodsNm;
        _requestBody["merchantToken"] = merchantToken;  // Merchant token included in body

        return this;
    }

    //FOR VA
    public NicepayRequestBuilder setBankCd(string bankCd){
        _requestBody["bankCd"] = bankCd;

        return this;
    }


    //FOR CARD
    public NicepayRequestBuilder setCreditCard(string instmntType, string instmntMon, string recurrOpt){
        _requestBody["instmntType"] = instmntType;
        _requestBody["instmntMon"] = instmntMon;
        _requestBody["recurrOpt"] = recurrOpt;

        return this;
    }

    //FOR QRIS
    public NicepayRequestBuilder setMitraCd(string mitraCd){
        _requestBody["mitraCd"] = mitraCd;

        return this;
    }

     public NicepayRequestBuilder setShopId(string shopId){
        _requestBody["shopId"] = shopId;

        return this;
    }

    // FOR Payout
     public NicepayRequestBuilder setPayout(string accountNo,string benefNm,string benefStatus,
     string benefType, string reservedDt, string reservedTm, string benefPhone, string payoutMethod,
     string timeStamp,string iMid, string amt, string referenceNo, string merchantToken){
        _requestBody["accountNo"] = accountNo;
        _requestBody["benefNm"] = benefNm;
        _requestBody["benefStatus"] = benefStatus;
        _requestBody["benefType"] = benefType;
        _requestBody["reservedDt"] = reservedDt;
        _requestBody["reservedTm"] = reservedTm;
        _requestBody["benefPhone"] = benefPhone;
        _requestBody["payoutMethod"] = payoutMethod;
        _requestBody["timeStamp"] = timeStamp;
        _requestBody["iMid"] = iMid;
        _requestBody["amt"] = amt;
        _requestBody["referenceNo"] = referenceNo;
        _requestBody["merchantToken"] = merchantToken;


        return this;
    }
   
    public NicepayRequestBuilder setPaymentExp(string paymentExpDt, string paymentExpTm){
        _requestBody["paymentExpDt"] = paymentExpDt;
        _requestBody["paymentExpTm"] = paymentExpTm;

        return this;
    }
    // Set Billing Info
    public NicepayRequestBuilder SetBillingInfo(string billingNm, string billingPhone, string billingEmail, string billingAddr, string billingCity, string billingState, string billingPostCd, string billingCountry)
    {
        _requestBody["billingNm"] = billingNm;
        _requestBody["billingPhone"] = billingPhone;
        _requestBody["billingEmail"] = billingEmail;
        _requestBody["billingAddr"] = billingAddr;
        _requestBody["billingCity"] = billingCity;
        _requestBody["billingState"] = billingState;
        _requestBody["billingPostCd"] = billingPostCd;
        _requestBody["billingCountry"] = billingCountry;

        return this;
    }

    //CHECK STATUS
    public NicepayRequestBuilder SetCheckStatus(string iMid, string timeStamp, string tXid,string amt, string referenceNo, string merchantToken)
    {
        _requestBody["timeStamp"] = timeStamp;
        _requestBody["iMid"] = iMid;
        _requestBody["tXid"] = tXid;
        _requestBody["amt"] = amt;
        _requestBody["referenceNo"] = referenceNo;
        _requestBody["merchantToken"] = merchantToken;  // Merchant token included in body

        return this;
    }


    //CANCEL 
    public NicepayRequestBuilder SetCancel(string iMid, string timeStamp, string tXid,string amt,
     string merchantToken, string payMethod,string cancelType, string cancelMsg, string cancelUserId
     ,string cancelServerIp, string cancelRetryCnt)
    {
        _requestBody["timeStamp"] = timeStamp;
        _requestBody["iMid"] = iMid;
        _requestBody["tXid"] = tXid;
        _requestBody["amt"] = amt;
        _requestBody["merchantToken"] = merchantToken;  // Merchant token included in body
        _requestBody["payMethod"] = payMethod;
        _requestBody["cancelType"] = cancelType;
        _requestBody["cancelMsg"] = cancelMsg;
        _requestBody["cancelUserId"] = cancelUserId;
        _requestBody["cancelServerIp"] = cancelServerIp;
        _requestBody["cancelRetryCnt"] = cancelRetryCnt;

        return this;
    }

   //CHECK STATUS PAYOUT
    public NicepayRequestBuilder SetPayoutCheckStatus(string iMid, string timeStamp, string tXid,string accountNo, string merchantToken)
    {
        _requestBody["timeStamp"] = timeStamp;
        _requestBody["iMid"] = iMid;
        _requestBody["tXid"] = tXid;
        _requestBody["accountNo"] = accountNo;
        _requestBody["merchantToken"] = merchantToken;  // Merchant token included in body

        return this;
    }

    //CHECK STATUS PAYOUT
    public NicepayRequestBuilder SetPayoutStep(string iMid, string timeStamp, string tXid, string merchantToken)
    {
        _requestBody["timeStamp"] = timeStamp;
        _requestBody["iMid"] = iMid;
        _requestBody["tXid"] = tXid;
        _requestBody["merchantToken"] = merchantToken;  // Merchant token included in body

        return this;
    }

     public NicepayRequestBuilder SetPayoutBalance(string iMid, string timeStamp, string merchantToken)
    {
        _requestBody["timeStamp"] = timeStamp;
        _requestBody["iMid"] = iMid;
        _requestBody["merchantToken"] = merchantToken;  // Merchant token included in body

        return this;
    }

    // Set Delivery Info
    public NicepayRequestBuilder SetDeliveryInfo(string deliveryNm, string deliveryPhone, string deliveryAddr, string deliveryCity, string deliveryState, string deliveryPostCd, string deliveryCountry)
    {
        _requestBody["deliveryNm"] = deliveryNm;
        _requestBody["deliveryPhone"] = deliveryPhone;
        _requestBody["deliveryAddr"] = deliveryAddr;
        _requestBody["deliveryCity"] = deliveryCity;
        _requestBody["deliveryState"] = deliveryState;
        _requestBody["deliveryPostCd"] = deliveryPostCd;
        _requestBody["deliveryCountry"] = deliveryCountry;

        return this;
    }

    // Set DB Process URL
    public NicepayRequestBuilder SetDbProcessUrl(string dbProcessUrl)
    {
        _requestBody["dbProcessUrl"] = dbProcessUrl;
        return this;
    }

    // Set Cart Data
    public NicepayRequestBuilder SetCartData(string cartData)
    {
        _requestBody["cartData"] = cartData;
        return this;
    }

            // Set Callback URL
public NicepayRequestBuilder SetCallBackUrl(string callBackUrl)
{
    _requestBody["callBackUrl"] = callBackUrl;
    return this;
}

// Set VAT, Fee, NoTaxAmt
public NicepayRequestBuilder SetTaxInfo(string vat, string fee, string notaxAmt)
{
    _requestBody["vat"] = vat;
    _requestBody["fee"] = fee;
    _requestBody["notaxAmt"] = notaxAmt;
    return this;
}

// Set Description
public NicepayRequestBuilder SetDescription(string description)
{
    _requestBody["description"] = description;
    return this;
}

// Set Request Info
public NicepayRequestBuilder SetRequestInfo(string reqDt, string reqTm, string reqDomain, string reqServerIP, string reqClientVer)
{
    _requestBody["reqDt"] = reqDt;
    _requestBody["reqTm"] = reqTm;
    _requestBody["reqDomain"] = reqDomain;
    _requestBody["reqServerIP"] = reqServerIP;
    _requestBody["reqClientVer"] = reqClientVer;
    return this;
}

// Set User Info
public NicepayRequestBuilder SetUserInfo(string userIP, string userSessionID, string userAgent, string userLanguage)
{
    _requestBody["userIP"] = userIP;
    _requestBody["userSessionID"] = userSessionID;
    _requestBody["userAgent"] = userAgent;
    _requestBody["userLanguage"] = userLanguage;
    return this;
}

// Set Sellers
public NicepayRequestBuilder SetSellers(string sellers)
{
    _requestBody["sellers"] = sellers;
    return this;
}

// Set VA Expiry Date and Time
public NicepayRequestBuilder SetVaExpiry(string vacctValidDt, string vacctValidTm)
{
    _requestBody["vacctValidDt"] = vacctValidDt;
    _requestBody["vacctValidTm"] = vacctValidTm;
    return this;
}

// Set Pay Expiry (already exists as SetPaymentExp, skip)

// Set Miscellaneous Fix Account ID
public NicepayRequestBuilder SetMerFixAcctId(string merFixAcctId)
{
    _requestBody["merFixAcctId"] = merFixAcctId;
    return this;
}


    // Build and return the request body
    public Dictionary<string, object> Build()
    {
        return _requestBody;
    }
}
