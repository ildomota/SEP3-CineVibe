using Microsoft.EntityFrameworkCore;
using CineVibeAPI.Models;

namespace CineVibeAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieReviews> MovieReviews { get; set; }
        public DbSet<Snacks> Snacks { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<SnackOrder> SnackOrders { get; set; }
        public DbSet<LoyaltyPoints> LoyaltyPoints { get; set; }
        public DbSet<Notification> Notifications { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("cinevibe");

            modelBuilder.Entity<Admin>().ToTable("admin", "cinevibe");
            modelBuilder.Entity<Customer>().ToTable("customer", "cinevibe");
            modelBuilder.Entity<Movie>().ToTable("movies", "cinevibe");
            modelBuilder.Entity<MovieReviews>().ToTable("moviereviews", "cinevibe");
            modelBuilder.Entity<Snacks>().ToTable("snacks", "cinevibe");
            modelBuilder.Entity<SnackOrder>().ToTable("snackorders", "cinevibe");
            modelBuilder.Entity<Booking>().ToTable("bookings", "cinevibe");
            modelBuilder.Entity<LoyaltyPoints>().ToTable("loyaltypoints", "cinevibe");
            modelBuilder.Entity<Notification>().ToTable("notifications", "cinevibe");
           

            modelBuilder.Entity<MovieReviews>()
                .HasOne<Customer>()
                .WithMany()
                .HasForeignKey(mr => mr.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MovieReviews>()
                .HasOne<Movie>()
                .WithMany()
                .HasForeignKey(mr => mr.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SnackOrder>()
                .HasOne<Customer>()
                .WithMany()
                .HasForeignKey(so => so.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SnackOrder>()
                .HasOne<Snacks>()
                .WithMany()
                .HasForeignKey(so => so.SnackId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}