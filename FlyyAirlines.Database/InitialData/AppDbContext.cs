using FlyyAirlines.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyyAirlines.Database
{
    public partial class AppDbContext : IdentityDbContext<User>
    {
        partial void InitialPermissions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>().HasData(new Permission
            {
                Id = "621f9c31-727a-4bbd-a792-cf1eae6e793b",
                FullName = "Full access of the system",
                Name = "ADMIN"
            });
            modelBuilder.Entity<Permission>().HasData(new Permission
            {
                Id = "cf00952b-3eb2-4f8c-8688-86d42cae01f0",
                FullName = "Can add Admins and all Users",
                Name = "SUPERADMIN"
            });
            modelBuilder.Entity<Permission>().HasData(new Permission
            {
                Id = "9701b54a-3559-453c-aa15-7754c5cfd491",
                FullName = "Can view support Page",
                Name = "ISSUPPORT"
            });
            modelBuilder.Entity<Permission>().HasData(new Permission
            {
                Id = "658e77ee-22e3-433c-a93c-f1d04b51695b",
                FullName = "Have access to Employee Panel",
                Name = "EMPLOYEE"
            });
        }

        partial void InitialData(ModelBuilder modelBuilder)
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

            modelBuilder.Entity<User>().HasData(new User
                {
                    Id = "6e2575e0-aa31-4f6f-b203-a4921803186d",
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
