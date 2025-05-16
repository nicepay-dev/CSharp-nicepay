using System.Text;

namespace SignatureGenerator{
public class MerchantTokenBuilder
{
    private string timeStamp;
    private string iMid;
    private string refNo;
    private string amount;
    private string merchantKey;
    private string accountNo;
    private string txid;

    public MerchantTokenBuilder SetTimeStamp(string timeStamp)
    {
        this.timeStamp = timeStamp;
        return this;
    }

    public MerchantTokenBuilder SetAccountNo(string accountNo)
    {
        this.accountNo = accountNo;
        return this;
    }

    public MerchantTokenBuilder SetIMid(string iMid)
    {
        this.iMid = iMid;
        return this;
    }

    public MerchantTokenBuilder SetRefNo(string refNo)
    {
        this.refNo = refNo;
        return this;
    }

    public MerchantTokenBuilder SetAmount(string amount)
    {
        this.amount = amount;
        return this;
    }

    public MerchantTokenBuilder SetTXid(string txid)
    {
        this.txid = txid;
        return this;
    }

    public MerchantTokenBuilder SetMerchantKey(string merchantKey)
    {
        this.merchantKey = merchantKey;
        return this;
    }

    // Method untuk menghasilkan merchantToken
    public string BuildMerchantToken()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(timeStamp);
        stringBuilder.Append(iMid);
        stringBuilder.Append(refNo); 
        stringBuilder.Append(amount);
        stringBuilder.Append(merchantKey);

        // Menghasilkan merchantToken dengan enkripsi SHA256

        return SignatureGeneratorUtils.SHA256Util.Encrypt(stringBuilder.ToString());

    }


    public string BuildPayoutMerchantToken()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(timeStamp);
        stringBuilder.Append(iMid);
        stringBuilder.Append(amount);
        stringBuilder.Append(accountNo); 
        stringBuilder.Append(merchantKey);

        // Menghasilkan merchantToken dengan enkripsi SHA256

        return SignatureGeneratorUtils.SHA256Util.Encrypt(stringBuilder.ToString());

    }


    public string BuildPayoutStatusMerchantToken()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(timeStamp);
        stringBuilder.Append(iMid);
        stringBuilder.Append(txid);
        stringBuilder.Append(accountNo); 
        stringBuilder.Append(merchantKey);

        // Menghasilkan merchantToken dengan enkripsi SHA256

        return SignatureGeneratorUtils.SHA256Util.Encrypt(stringBuilder.ToString());

    }

      public string BuildPayoutStepMerchantToken()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(timeStamp);
        stringBuilder.Append(iMid);
        stringBuilder.Append(txid);
        stringBuilder.Append(merchantKey);

        // Menghasilkan merchantToken dengan enkripsi SHA256

        return SignatureGeneratorUtils.SHA256Util.Encrypt(stringBuilder.ToString());

    }


    public string BuildPayoutBalanceMerchantToken()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(timeStamp);
        stringBuilder.Append(iMid);
        stringBuilder.Append(merchantKey);

        // Menghasilkan merchantToken dengan enkripsi SHA256

        return SignatureGeneratorUtils.SHA256Util.Encrypt(stringBuilder.ToString());

    }

       public string BuildCancelMerchantToken()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(timeStamp);
        stringBuilder.Append(iMid);
        stringBuilder.Append(txid);
        stringBuilder.Append(amount);
        stringBuilder.Append(merchantKey);

        // Menghasilkan merchantToken dengan enkripsi SHA256

        return SignatureGeneratorUtils.SHA256Util.Encrypt(stringBuilder.ToString());

    }
    
    // Method untuk membangun payload secara keseluruhan (optional)
    // public string BuildPayload()
    // {
    //     return $"TimeStamp: {timeStamp}, IMid: {iMid}, RefNo: {refNo}, Amount: {amount}, MerchantKey: {merchantKey}";
    // }


    }
}