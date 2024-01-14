using Chat.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chat.Api.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Message> Messages { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Message>().HasIndex(x => x.Id).IsUnique();
            builder.Entity<Group>().HasIndex(x => x.Name).IsUnique();
        }
    }
}
