public class PayoutBuilder
{
    private Dictionary<string, object> _requestBody = new Dictionary<string, object>();


        
    public PayoutBuilder SetPayoutV2(
    string iMid, 
    string timeStamp, 
    string payMethod, 
    string currency, 
    string amt, 
    string referenceNo, 
    string goodsNm, 
    string merchantToken,
    string shopId,
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
        _requestBody["shopId"] = shopId;;
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
    public PayoutBuilder SetPayoutSnap(
    string merchantId, 
    string beneficiaryAccountNo, 
    string beneficiaryName, 
    string beneficiaryPhone,
    string payoutMethod, 
    string beneficiaryBankCode,  
    string partnerReferenceNo,
    string beneficiaryCustomerType,
    string beneficiaryCustomerResidence, 
    string reservedDt,
    string reservedTm,
    string value,
    string currency,
    string description)
    {
        _requestBody["partnerReferenceNo"] = partnerReferenceNo;
        _requestBody["merchantId"] = merchantId;
        _requestBody["beneficiaryAccountNo"] = beneficiaryAccountNo;
        _requestBody["beneficiaryName"] = beneficiaryName;
        _requestBody["beneficiaryPhone"] = beneficiaryPhone;
        _requestBody["payoutMethod"] = payoutMethod;
        _requestBody["beneficiaryBankCode"] = beneficiaryBankCode;
        _requestBody["beneficiaryCustomerType"] = beneficiaryCustomerType;
        _requestBody["beneficiaryCustomerResidence"] = beneficiaryCustomerResidence;
        _requestBody["reservedDt"] = reservedDt;
        _requestBody["reservedTm"] = reservedTm;
        _requestBody["description"] = description;

        _requestBody["amount"] = new Dictionary<string, object>
        {
            ["value"] = value,
            ["currency"] = currency
        };

        return this;
    }

    public PayoutBuilder SetApprovePayoutSnap(
    string originalReferenceNo, 
    string originalPartnerReferenceNo, 
    string merchantId)
    {
        _requestBody["originalReferenceNo"] = originalReferenceNo;
        _requestBody["originalPartnerReferenceNo"] = originalPartnerReferenceNo;
        _requestBody["merchantId"] = merchantId;
        

        return this;
    }


    public PayoutBuilder SetBalancePayoutSnap(
    string accountNo, 
    string msId)
    {
        _requestBody["accountNo"] = accountNo;

        _requestBody["additionalInfo"] = new Dictionary<string, object>
        {
            ["msId"] = msId
        };

        return this;
    }

    public PayoutBuilder SetRejectCancelPayoutSnap(
    string originalReferenceNo, 
    string originalPartnerReferenceNo, 
    string merchantId)
    {
        _requestBody["originalReferenceNo"] = originalReferenceNo;
        _requestBody["originalPartnerReferenceNo"] = originalPartnerReferenceNo;
        _requestBody["merchantId"] = merchantId;

        return this;
    }

    
    public Dictionary<string, object> Build()
    {
        return _requestBody;
    }
}