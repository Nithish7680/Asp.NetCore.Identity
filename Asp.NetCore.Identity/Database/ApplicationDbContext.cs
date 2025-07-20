using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Asp.NetCore.Identity.Database
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasIndex(e => e.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique();
                entity.HasIndex(e => e.NormalizedEmail).HasDatabaseName("EmailIndex").IsUnique();
                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

            });
            modelBuilder.HasDefaultSchema("user_identity");
        }
    }
}
