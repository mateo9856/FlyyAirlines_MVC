using FlyyAirlines.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace FlyyAirlines.Database
{
    public partial class AppDbContext : IdentityDbContext<User>
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
        public DbSet<Permission> Permissions { get; set; }
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
            modelBuilder.Entity<Permission>().Property(b => b.Id)
                .HasColumnName("PermissionId");

            InitialData(modelBuilder);

            InitialPermissions(modelBuilder);

            modelBuilder.Entity<User>()
               .HasMany(p => p.Permissions)
               .WithMany(p => p.Users)
               .UsingEntity(j => j.HasData(new { UsersId = "6e2575e0-aa31-4f6f-b203-a4921803186d", PermissionsId = "cf00952b-3eb2-4f8c-8688-86d42cae01f0" }));
        }

        partial void InitialData(ModelBuilder modelBuilder);

        partial void InitialPermissions(ModelBuilder modelBuilder);

    }
}
