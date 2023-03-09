using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }


    }
}
