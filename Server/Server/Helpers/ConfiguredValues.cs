namespace Server.Helpers


{
    // Configuration interface, for implemantation
    public interface IConfiguredValues
    {
        string GetSecretKey();
        string GetClient();
        string GetServer();
    }

    // Class to define configurations values for this project
    public class ConfiguredValues : IConfiguredValues
    {
        public string GetSecretKey()
        {
            return "123BossVictor123";
        }

        public string GetClient()
        {
            return "http://localhost:4200";
        }

        public string GetServer()
        {
            return "https://localhost:7099/";
        }
    }
}
