using Microsoft.EntityFrameworkCore;
using XinWebAPI.Data.XinIdentity;
using XinWebAPI.Models.PlayGround;

namespace XinWebAPI.Data.PlayGround
{
    public class PlayGroundDBContext : XinIdentityDBContext
    {
        public PlayGroundDBContext(DbContextOptions<PlayGroundDBContext> context) : base(context)
        {
        }

        // register the classes
        public DbSet<Student> Student { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Customer> Customer { get; set; }

        //public DbSet<XinPublisher> XinPublisher { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //    modelBuilder.Entity<Customer>(entity =>
            //    {
            //        entity.HasKey(e => e.Id);
            //        entity.Property(e => e.Name).IsRequired();
            //        entity.Property(e => e.Industry).IsRequired();
            //    });

            //    //modelBuilder.Entity<Book>(entity =>
            //    //{
            //    //    entity.HasKey(e => e.ISBN);
            //    //    entity.Property(e => e.Title).IsRequired();
            //    //    entity.HasOne(d => d.Publisher)
            //    //      .WithMany(p => p.Books);
            //    //});
         }
     }
}
