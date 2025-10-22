using FirstFlyProject.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace FirstFlyProject.Data
{
    
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {

            }
            public DbSet<User> Users { get; set; }
            public DbSet<TravelPackage> TravelPackages { get; set; }
            public DbSet<Review> Reviews { get; set; }
            public DbSet<Payment> Payments { get; set; }
            public DbSet<Insurance> Insurances { get; set; }
            public DbSet<Booking> Bookings { get; set; }
            public DbSet<AssistanceRequest> AssistanceRequests { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<TravelAgent> TravelAgents { get; set; }
        public DbSet<CardDetail> CardDetail { get; set; }



            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // USER ↔ BOOKING (One-to-Many)
                modelBuilder.Entity<Booking>()
                    .HasOne(b => b.User)
                    .WithMany(u => u.Bookings)
                    .HasForeignKey(b => b.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                // USER ↔ REVIEW (One-to-Many)
                modelBuilder.Entity<Review>()
                    .HasOne(r => r.User)
                    .WithMany(u => u.Reviews)
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                // USER ↔ PAYMENT (One-to-Many)
                modelBuilder.Entity<Payment>()
                    .HasOne(p => p.User)
                    .WithMany(u => u.Payments)
                    .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                // USER ↔ INSURANCE (One-to-Many)
                modelBuilder.Entity<Insurance>()
                    .HasOne(i => i.User)
                    .WithMany(u => u.Insurances)
                    .HasForeignKey(i => i.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                // USER ↔ ASSISTANCE REQUEST (One-to-Many)
                modelBuilder.Entity<AssistanceRequest>()
                    .HasOne(ar => ar.User)
                    .WithMany(u => u.AssistanceRequests)
                    .HasForeignKey(ar => ar.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                // BOOKING ↔ REVIEW (One-to-Many)
                modelBuilder.Entity<Review>()
                    .HasOne(r => r.Booking)
                    .WithMany(b => b.Reviews)
                    .HasForeignKey(r => r.BookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                // BOOKING ↔ PAYMENT (One-to-Many)
                modelBuilder.Entity<Payment>()
                    .HasOne(p => p.Booking)
                    .WithMany(b => b.Payments)
                    .HasForeignKey(p => p.BookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                // BOOKING ↔ INSURANCE (One-to-Many)
                modelBuilder.Entity<Insurance>()
                    .HasOne(i => i.Booking)
                    .WithMany(b => b.Insurances)
                    .HasForeignKey(i => i.BookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                // BOOKING ↔ TRAVEL PACKAGE (Many-to-One)
                modelBuilder.Entity<Booking>()
                    .HasOne(b => b.Package)
                    .WithMany(tp => tp.Bookings)
                    .HasForeignKey(b => b.PackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                modelBuilder.Entity<User>()
                    .HasOne(u=>u.Customer)
                    .WithOne(c=>c.Cust) // or .WithOne() depending on your model
                    .HasForeignKey<Customer>(c=>c.CustomerID);
                modelBuilder.Entity<User>()
                    .HasOne(u => u.TravelAgent)
                    .WithOne(t=>t.Agent) // or .WithOne() depending on your model
                    .HasForeignKey<TravelAgent>(t=>t.TravelAgentID);

            //TravelAgent - TravelPackage (one -to-many)
            modelBuilder.Entity<TravelPackage>().
                HasOne(tp => tp.Agent)
                .WithMany(a => a.TravelPackages)
                .HasForeignKey(a => a.TravelAgentID)
                .OnDelete(DeleteBehavior.NoAction);
                
        }

        }
    
}
