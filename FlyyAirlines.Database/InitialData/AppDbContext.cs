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
                Id = 1,
                FullName = "Full access of the system",
                Name = "ADMIN"
            });
            modelBuilder.Entity<Permission>().HasData(new Permission
            {
                Id = 2,
                FullName = "Can add Admins and all Users",
                Name = "SUPERADMIN"
            });
            modelBuilder.Entity<Permission>().HasData(new Permission
            {
                Id = 3,
                FullName = "Can view support Page",
                Name = "ISSUPPORT"
            });
            modelBuilder.Entity<Permission>().HasData(new Permission
            {
                Id = 4,
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
