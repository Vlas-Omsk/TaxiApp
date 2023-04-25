using Microsoft.EntityFrameworkCore;
using TaxiApp.DAL.Entities;

namespace TaxiApp.DAL
{
    public sealed class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasKey(e => e.Login).HasName("PK_UserLogin");

                entity.ToTable("User");

                entity.Property(e => e.Login)
                    .HasMaxLength(40);
                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(32);
                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasConversion<string>()
                    .HasMaxLength(40);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;

            optionsBuilder.UseSqlServer();
        }
    }
}
