using System.Net;

public static class NICEPayBuilder
{
    private static bool isProduction = false; // Set to true for production

    private static bool isCloudServer  =  false;


public static string GetSandboxCloud(){
    return "https://dev-services.nicepay.co.id/";
}

public static string GetProductionCloud(){
    return "https://services.nicepay.co.id/";
}

  public static string GetSandboxBaseUrl()
    {
        return "https://dev.nicepay.co.id/";
    }

    public static string GetProductionBaseUrl()
    {
        return "https://www.nicepay.co.id/";
    }
    
   public static string GetSnapApiURL()
    {
        if (isCloudServer)
        {
            return isProduction ? GetProductionCloud() : GetSandboxCloud();
        }
        else
        {
            return isProduction ? GetProductionBaseUrl() : GetSandboxBaseUrl();
        }
    }


    // Optionally, add a method to set the environment
    public static void SetEnvironment(bool production, bool cloudServer)
    {
        isProduction = production;
        isCloudServer = cloudServer;
    }

}