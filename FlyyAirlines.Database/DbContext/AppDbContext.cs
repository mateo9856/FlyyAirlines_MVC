using FlyyAirlines.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace FlyyAirlines.Database
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Airplane> Airplanes { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<News> QuickNews { get; set; }
        public DbSet<Message> Messages { get; set; }
        public override DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Airplane>().Property(b => b.Id)
                .HasColumnName("AirplaneId");
            modelBuilder.Entity<Employee>().Property(b => b.Id)
            .HasColumnName("EmployeeId");
            modelBuilder.Entity<Flight>().Property(b => b.Id)
            .HasColumnName("FlightsId");
            modelBuilder.Entity<Reservation>().Property(b => b.Id)
            .HasColumnName("ReservationId");
            modelBuilder.Entity<User>().Property(b => b.Id)
                .HasColumnName("UserId");
            modelBuilder.Entity<News>().Property(b => b.Id)
                .HasColumnName("NewsId");

            InitialData(modelBuilder);

        }

        protected void InitialData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Admin",
                NormalizedName = "ADMIN"
            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "SuperAdmin",
                NormalizedName = "SUPERADMIN"
            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Employee",
                NormalizedName = "EMPLOYEE"
            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "User",
                NormalizedName = "USER"
            });

            var passowrdHash = new PasswordHasher<User>();

            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "Super@Dmin",
                    NormalizedUserName = "SUPER@DMIN",
                    Name = "Mateusz",
                    Surname = "Magdziak",
                    PasswordHash = passowrdHash.HashPassword(null, "$M@teuszAdmin4"),
                    Role = "SuperAdmin",
                    Email = "mateuszAdmin@flyy.com",
                    NormalizedEmail = "MATEUSZADMIN@FLYY.COM",
                    EmailConfirmed = false,
                    SecurityStamp = string.Empty
                });
        }

    }
}
