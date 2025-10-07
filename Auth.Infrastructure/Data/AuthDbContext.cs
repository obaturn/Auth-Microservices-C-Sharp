
using Microsoft.EntityFrameworkCore;
using Auth.Domain.Entities;

namespace Auth.Infrastructure.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        // EF requires DbSet properties to be non-null for migrations; mark with = null! to satisfy nullable refs
        public DbSet<User> Users { get; set; } = null!;
    }
}