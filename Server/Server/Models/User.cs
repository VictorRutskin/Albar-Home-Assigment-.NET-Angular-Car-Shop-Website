namespace Server.Models
{
    // My user model, no validations needed because i do not create new users in this website.
    public class User
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
