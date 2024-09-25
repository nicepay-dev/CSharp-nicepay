public class TestingConstant
{
    public string ClientId { get; private set; }
    public string ClientSecret { get; private set; }
    public string PrivateKey { get; private set; }
    public string ChannelId { get; private set; }


    // INSERT YOUR CREDENTIAL HERE 
    public TestingConstant()
    {
        ClientId = "";
        ClientSecret = "";
        PrivateKey = "";
        ChannelId = "123456";
    }
}
