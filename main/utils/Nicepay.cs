public static class NICEPayBuilder
{
    private static bool isProduction = false; // Set to true for production


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
        if (isProduction)
        {
            return GetProductionBaseUrl();
        }
        else
        {
            return GetSandboxBaseUrl();
        }
    }

    // Optionally, add a method to set the environment
    public static void SetEnvironment(bool production)
    {
        isProduction = production;
    }
}