using System.ComponentModel.DataAnnotations;

namespace SignatureGenerator;
public class TestingConstant
{
    public string ClientId { get; private set; }
    public string ClientSecret { get; private set; }
    public string PrivateKey { get; private set; }
    public string ChannelId { get; private set; }
     public string Timestamp { get; private set; }


    // INSERT YOUR CREDENTIAL HERE 
    public TestingConstant()
    {
        ClientId = "";
        ClientSecret = "";
        PrivateKey = "";
        ChannelId = "";
        Timestamp =  DateTimeOffset.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
    }
    
}


public class TestingConstantService
    {
        public string ClientId { get; private set; }
        public string ClientSecret { get; private set; }
        public string PrivateKey { get; private set; }
        public string ChannelId { get; private set; }
        public string Timestamp { get; private set; }
        public string MerKey {get; private set;}

        public TestingConstantService()
        {
            ClientId = "";
            ClientSecret = "";
            PrivateKey = "";
            ChannelId = "";
            MerKey = "";
            Timestamp = DateTimeOffset.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
        }
    }

    public class TestingConstantPayout
    {
        public string ClientId { get; private set; }
        public string ClientSecret { get; private set; }
        public string PrivateKey { get; private set; }
        public string ChannelId { get; private set; }
        public string Timestamp { get; private set; }
        public string MerKey {get; private set;}

        public TestingConstantPayout()
        {
            ClientId = "";
            ClientSecret = "";
            PrivateKey = "";
            ChannelId = "";
            MerKey = "";
            Timestamp = DateTimeOffset.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
        }
    }