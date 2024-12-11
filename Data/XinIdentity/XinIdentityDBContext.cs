using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using XinWebAPI.Models.XinIdentity;
using System.Net;

namespace XinWebAPI.Data.XinIdentity
{
    public class XinIdentityDBContext : IdentityDbContext<XinUser>
    {

        public XinIdentityDBContext(DbContextOptions<XinIdentityDBContext> context) : base(context)
        {
        }

        public XinIdentityDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
