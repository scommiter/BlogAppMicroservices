using Microsoft.EntityFrameworkCore;

namespace Notification.Api.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Entities.Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Entities.Notification>().HasIndex(x => x.Id).IsUnique();
        }
    }
}
