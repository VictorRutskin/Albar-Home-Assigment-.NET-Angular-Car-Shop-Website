namespace Server.Services
{
    public static class Paths
    {
        //string folderName = Path.Combine("wwwroot", "Images", "Cars");
        //string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

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
