using Newtonsoft.Json;
namespace SignatureGenerator
{
    public class GrantTypeRequestExample
    {
        public static void Main(string[] args)
        {
            var builder = new AccessTokenBuilder("client_credentials");

            // Tambahkan informasi tambahan jika diperlukan
            builder.SetAdditionalInfo(new {});

            // Bangun objek dan konversi ke JSON
            var requestBody = builder.Build();
            string jsonBody = JsonConvert.SerializeObject(requestBody, Formatting.Indented);

            // Cetak hasil JSON
            Console.WriteLine(jsonBody);
        }
    }
}
