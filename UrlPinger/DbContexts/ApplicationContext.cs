using Microsoft.EntityFrameworkCore;
using UrlPinger.Models;

namespace UrlPinger.DbContexts
{
    public class ApplicationContext : DbContext
    {
        public DbSet<RemoteAddress> RemoteAddresses { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RemoteAddress>()
                .HasIndex(u => u.Url)
                .IsUnique();
        }
    }
}
