public class V1Builder
{
    private Dictionary<string, object> _requestBody = new Dictionary<string, object>();


        
    public V1Builder SetRedirect(
    string timeStamp,
    string iMid,
    string payMethod,
    string currency,
    string merchantToken,
    string referenceNo,

    string dbProcessUrl,

    string instmntType,
    string instmntMon,
    string recurrOpt,

    string userIP,
    string userLanguage,
    string userAgent,

    string amt,
    string cartData,
    string goodsNm,
    string billingNm,
    string billingPhone,
    string billingEmail,
    string billingAddr,
    string billingCity,
    string billingState,
    string billingCountry,
    string billingPostCd,

    //    PAYMENT

    string callBackUrl,

    string description,
    string deliveryNm,
    string deliveryPhone,
    string deliveryAddr,
    string deliveryCity,
    string deliveryState,
    string deliveryPostCd,
    string deliveryCountry,

    string reqDomain,
    string reqServerIP,
    string reqClientVer,
    string userSessionID,
    string sellers,
    string mitraCd,

    string vat,
    string fee,
    string notaxAmt,
    string reqDt,
    string reqTm,
    string bankCd,
    string vacctValidDt,
    string vacctValidTm,
    string payValidDt,
    string payValidTm,
    string merFixAcctId,
    string paymentExpDt,
    string paymentExpTm,
    string shopId,
    string onePassToken,
    string cardCvv

    )
    {
    _requestBody["timeStamp"] = timeStamp;
    _requestBody["iMid"] = iMid;
    _requestBody["payMethod"] = payMethod;
    _requestBody["currency"] = currency;
    _requestBody["merchantToken"] = merchantToken;
    _requestBody["referenceNo"] = referenceNo;

    _requestBody["dbProcessUrl"] = dbProcessUrl;

    _requestBody["instmntType"] = instmntType;
    _requestBody["instmntMon"] = instmntMon;
    _requestBody["recurrOpt"] = recurrOpt;

    _requestBody["userIP"] = userIP;
    _requestBody["userLanguage"] = userLanguage;
    _requestBody["userAgent"] = userAgent;

    _requestBody["amt"] = amt;
    _requestBody["cartData"] = cartData;
    _requestBody["goodsNm"] = goodsNm;
    _requestBody["billingNm"] = billingNm;
    _requestBody["billingPhone"] = billingPhone;
    _requestBody["billingEmail"] = billingEmail;
    _requestBody["billingAddr"] = billingAddr;
    _requestBody["billingCity"] = billingCity;
    _requestBody["billingState"] = billingState;
    _requestBody["billingCountry"] = billingCountry;
    _requestBody["billingPostCd"] = billingPostCd;

// PAYMENT
    _requestBody["callBackUrl"] = callBackUrl;

    _requestBody["description"] = description;
    _requestBody["deliveryNm"] = deliveryNm;
    _requestBody["deliveryPhone"] = deliveryPhone;
    _requestBody["deliveryAddr"] = deliveryAddr;
    _requestBody["deliveryCity"] = deliveryCity;
    _requestBody["deliveryState"] = deliveryState;
    _requestBody["deliveryPostCd"] = deliveryPostCd;
    _requestBody["deliveryCountry"] = deliveryCountry;

    _requestBody["reqDomain"] = reqDomain;
    _requestBody["reqServerIP"] = reqServerIP;
    _requestBody["reqClientVer"] = reqClientVer;
    _requestBody["userSessionID"] = userSessionID;
    _requestBody["sellers"] = sellers;
    _requestBody["mitraCd"] = mitraCd;

    _requestBody["vat"] = vat;
    _requestBody["fee"] = fee;
    _requestBody["notaxAmt"] = notaxAmt;
    _requestBody["reqDt"] = reqDt;
    _requestBody["reqTm"] = reqTm;
    _requestBody["bankCd"] = bankCd;
    _requestBody["vacctValidDt"] = vacctValidDt;
    _requestBody["vacctValidTm"] = vacctValidTm;
    _requestBody["payValidDt"] = payValidDt;
    _requestBody["payValidTm"] = payValidTm;
    _requestBody["merFixAcctId"] = merFixAcctId;
    _requestBody["paymentExpDt"] = paymentExpDt;
    _requestBody["paymentExpTm"] = paymentExpTm;
    _requestBody["shopId"] = shopId;
    _requestBody["onePassToken"] = onePassToken;
    _requestBody["cardCvv"] = cardCvv;

        return this;
    }


    
        public V1Builder SetCardToken(string iMid, string referenceNo, string amt, string cardNo, string cardExpYymm, string merchantToken)
    {
        _requestBody["iMid"] = iMid;
        _requestBody["referenceNo"] = referenceNo;
        _requestBody["amt"] = amt;
        _requestBody["cardNo"] = cardNo;
        _requestBody["cardExpYymm"] = cardExpYymm;
        _requestBody["merchantToken"] = merchantToken;  // Merchant token included in body
        
        return this;
    }



    public V1Builder SetCheckStatusV1(
    string iMid,
    string merchantToken,
    string tXid,
    string amt,
    string referenceNo
    
    )
    {
    _requestBody["tXid"] = tXid;
    _requestBody["iMid"] = iMid;
    _requestBody["merchantToken"] = merchantToken;
    _requestBody["referenceNo"] = referenceNo;
    _requestBody["amt"] = amt;
    

        return this;
    }

    public V1Builder SetCancelV1(
    string iMid,
    string merchantToken,
    string tXid,
    string payMethod,
    string cancelType,
    string cancelMsg,
    string amt
    
    )
    {
    _requestBody["tXid"] = tXid;
    _requestBody["iMid"] = iMid;
    _requestBody["merchantToken"] = merchantToken;
    _requestBody["amt"] = amt;
    _requestBody["payMethod"] = payMethod;
    _requestBody["cancelType"] = cancelType;
    _requestBody["cancelMsg"] = cancelMsg;
    

        return this;
    }

    public Dictionary<string, object> Build()
    {
        return _requestBody;
    }
}