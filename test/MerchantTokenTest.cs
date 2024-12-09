using Moq;
using NUnit.Framework;
using System.Security.Cryptography;
using System.Text;

public class MerchantTokenTest
{
    [Test]
    public void TestGenerateMerchantToken()
    {
        // Arrange: Buat mock objek requestLinkEnable
        var requestLinkEnableMock = new Mock<IRequestLinkEnable>();
        requestLinkEnableMock.Setup(r => r.GetTimeStamp()).Returns("20241112150830");
        requestLinkEnableMock.Setup(r => r.GetIMid()).Returns("DIGITAL3DU");
        requestLinkEnableMock.Setup(r => r.GetTxid()).Returns("TX1234567890");
        requestLinkEnableMock.Setup(r => r.GetAmount()).Returns("100000");

        string merchantKey = "h4B24oil4YhlWk8FxZL5YgemKLsLWm7Cbc3u8i2ioZor0NBKeyENVbk/gcs9xpIauc97W9JDp8As7foArfzPzQ==";

        // Expected merchantToken value
        //string expectedToken = SHA256Util.Encrypt("20240101123000IONPAYTESTTX123456789010000033F49GnCMS1mFYlGXisbUDzVf2ATWCl9k3R++d5hDd3Frmuos/XLx8XhXpe+LDYAbpGKZYSwtlyyLOtS/8aD7A==");

        // Act: Buat token menggunakan kode yang ingin diuji
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(requestLinkEnableMock.Object.GetTimeStamp());
        stringBuilder.Append(requestLinkEnableMock.Object.GetIMid());
        stringBuilder.Append(requestLinkEnableMock.Object.GetTxid());
        stringBuilder.Append(requestLinkEnableMock.Object.GetAmount());
        stringBuilder.Append(merchantKey);
        string merchantToken = SHA256Util.Encrypt(stringBuilder.ToString());

        // Assert: Verifikasi apakah hasil token sesuai dengan yang diharapkan
        //Assert.AreEqual(expectedToken, merchantToken);
        
        Console.WriteLine("Hasil Merchant Token: " + merchantToken);
    }
}

public static class SHA256Util
{
    public static string Encrypt(string value)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(value));
            StringBuilder result = new StringBuilder();
            foreach (byte b in bytes)
            {
                result.Append(b.ToString("x2"));
            }
            return result.ToString();
        }
    }
}

public interface IRequestLinkEnable
{
    string GetTimeStamp();
    string GetIMid();
    string GetTxid();
    string GetAmount();
}
