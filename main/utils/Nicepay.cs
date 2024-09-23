public static class NICEPayBuilder
{
    private static bool isProduction = false; // Set to true for production

    public static string GetSnapApiURL()
    {
        if (isProduction)
        {
            return ApiEndpoints.GetProductionBaseUrl();
        }
        else
        {
            return ApiEndpoints.GetSandboxBaseUrl();
        }
    }

    // Optionally, add a method to set the environment
    public static void SetEnvironment(bool production)
    {
        isProduction = production;
    }
}