namespace Server.Models
{
    public class User
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
