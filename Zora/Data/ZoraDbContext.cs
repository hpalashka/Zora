using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Zora.Web.Data.Models;

namespace Zora.Web.Data
{
    public class ZoraDbContext : DbContext

    {
        public ZoraDbContext(DbContextOptions<ZoraDbContext> options)
           : base(options)
        {
        }


        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<StoreImage> StoreImages { get; set; }

        public DbSet<HomePageCover> HomePageCovers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
          
        }

    }
}