using Microsoft.EntityFrameworkCore;
using RestaurantReservationApp.Models;

namespace RestaurantReservationApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User-Role relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .IsRequired();

            // User-Customer relationship
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.User)
                .WithOne()
                .HasForeignKey<Customer>(c => c.UserId)
                .IsRequired();

            // Restaurant-Owner (User) relationship
            modelBuilder.Entity<Restaurant>()
                .HasOne(r => r.Owner)
                .WithMany()
                .HasForeignKey(r => r.OwnerId)
                .IsRequired();

            // Restaurant-Review relationship
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Restaurant)
                .WithMany(r => r.Reviews)
                .HasForeignKey(r => r.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);

            // Customer-Review relationship
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Restaurant-Reservation relationship
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Restaurant)
                .WithMany(r => r.Reservations)
                .HasForeignKey(r => r.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);

            // Customer-Reservation relationship
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.CustomerId)
                .IsRequired();

            // Configure length constraints
            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .Property(r => r.RoleName)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Restaurant>()
                .Property(r => r.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Restaurant>()
                .Property(r => r.Address)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<Restaurant>()
                .Property(r => r.Description)
                .HasMaxLength(500);

            modelBuilder.Entity<Restaurant>()
                .Property(r => r.OpeningHours)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Review>()
                .Property(r => r.Comment)
                .HasMaxLength(1000);

            // Additional constraints
            modelBuilder.Entity<Review>()
                .Property(r => r.Rating)
                .IsRequired();
        }
    }
}
