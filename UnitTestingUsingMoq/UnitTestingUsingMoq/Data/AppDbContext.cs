using Microsoft.EntityFrameworkCore;
using UnitTestingUsingMoq.Models;
namespace UnitTestingUsingMoq.Data
{
    public class AppDbContext: DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

            });

            // Configure Order entity
            modelBuilder.Entity<Order>(entity =>
            {

                entity.HasKey(o => o.OrderId);

                entity.Property(o => o.Product)
               .IsRequired()
               .HasMaxLength(200);

                entity.HasOne(o => o.User)
                   .WithMany(u => u.Orders)
                   .HasForeignKey(o => o.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            });
        }

    }
}
