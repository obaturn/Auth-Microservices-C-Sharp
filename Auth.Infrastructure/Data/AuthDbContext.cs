
namespace Auth.Infrastructure.Data
{
    public class AuthDbContext : DbContext{
        
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
    
}