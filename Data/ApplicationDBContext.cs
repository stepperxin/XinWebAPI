using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MySQLIdentityWebAPI.Models.Identity;
using System.Net;

namespace MySQLIdentityWebAPI.Data
{
    public class ApplicationDBContext : IdentityDbContext<IdentityUser>
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> context) : base(context)
        {
        }

        public DbSet<UserApiKey> UserApiKeys { get; set; }
        //// register the classes
        //public DbSet<Student> Student { get; set; }
        //public DbSet<Address> Address { get; set; }
        //public DbSet<Customer> Customer { get; set; }

        //public DbSet<IdentityUser> IdentityUser { get; set; }

        //public DbSet<XinPublisher> XinPublisher { get; set; }


    }
}
