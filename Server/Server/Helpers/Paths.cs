namespace Server.Helpers
{
    // Class to define paths that are used in many places, change here instead of each of them in case they move
    public static class Paths
    {
        public static string GetLogsFilePath()
        {

            string wwwrootFolder = Path.Combine("wwwroot", "Logger", "Logs.txt");

            // Creates missing folders if wwwroot doesnt exist
            if (!Directory.Exists(Path.Combine("wwwroot")))
            {
                Directory.CreateDirectory(Path.Combine("wwwroot"));
                Directory.CreateDirectory(Path.Combine("wwwroot", "Images"));
                Directory.CreateDirectory(Path.Combine("wwwroot", "Logger"));
            }

            string loggerFilePath = Path.Combine("wwwroot", "Logger", "Logs.txt");

            // Creates logger txt file if doesnt exist
            if (!File.Exists(loggerFilePath))
            {
                File.Create(loggerFilePath).Dispose();
            }

            return wwwrootFolder;
        }

        public static string GetLocalPath()
        {
            return Path.Combine("wwwroot", "Images", "Cars");
        }

        public static string GetGlobalPath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), GetLocalPath());
        }
    }
}
