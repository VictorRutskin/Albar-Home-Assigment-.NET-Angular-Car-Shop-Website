namespace Server.Services
{
    // Class to configure globally used values
    public static class ConfiguredValues
    {
        // Token Secret Key
        public static string GetSecretKey()
        {
            return "123BossVictor123";
        }

        // Client 
        public static string GetClient()
        {
            return "http://localhost:4200";
        }

        // Server
        public static string GetServer()
        {
            return "https://localhost:7099/";
        }
    }
}
