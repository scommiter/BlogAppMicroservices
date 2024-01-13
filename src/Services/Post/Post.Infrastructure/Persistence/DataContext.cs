using Microsoft.EntityFrameworkCore;
using Post.Domain.Entities;

namespace Post.Infrastructure.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Domain.Entities.Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<TreePath> TreePaths { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.Post>().HasKey(e => e.Id);

            modelBuilder.Entity<TreePath>()
                .HasKey(tp => new { tp.Ancestor, tp.Descendant });

            modelBuilder.Entity<TreePath>()
                .HasOne(tp => tp.AncestorComment)
                .WithMany()
                .HasForeignKey(tp => tp.Ancestor)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TreePath>()
                .HasOne(tp => tp.DescendantComment)
                .WithMany()
                .HasForeignKey(tp => tp.Descendant)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasMany(c => c.Ancestors)
                .WithOne(tp => tp.AncestorComment)
                .HasForeignKey(tp => tp.Ancestor)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasMany(c => c.Descendants)
                .WithOne(tp => tp.DescendantComment)
                .HasForeignKey(tp => tp.Descendant)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
