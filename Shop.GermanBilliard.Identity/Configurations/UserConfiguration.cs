using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Shop.GermanBilliard.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                 new ApplicationUser
                 {
                     Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                     Email = "admin@gmail.com",
                     NormalizedEmail = "ADMIN@GMAIL.COM",
                     FirstName = "System",
                     LastName = "Admin",
                     UserName = "admin@gmail.com",
                     NormalizedUserName = "ADMIN@GMAIL.COM",
                     PasswordHash = hasher.HashPassword(null, "PasswordAmind@"),
                     EmailConfirmed = true
                 },
                 new ApplicationUser
                 {
                     Id = "9e224968-33e4-4652-b7b7-8574d048cdb9",
                     Email = "user@gmail.com",
                     NormalizedEmail = "USER@GMAIL.COM",
                     FirstName = "System",
                     LastName = "User",
                     UserName = "user@gmail.com",
                     NormalizedUserName = "USER@GMAIL.COM",
                     PasswordHash = hasher.HashPassword(null, "Employee@"),
                     EmailConfirmed = true
                 }
            );
        }
    }
}
