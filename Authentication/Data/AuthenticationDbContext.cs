using Authentication.Models.User;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Data
{
    public class AuthenticationDbContext : DbContext
    {
        public AuthenticationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
