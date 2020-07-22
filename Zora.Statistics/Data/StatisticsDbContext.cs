using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Zora.Statistics.Data.Models;


namespace Zora.Statistics.Data
{
    public class StatisticsDbContext : DbContext
    {
        public StatisticsDbContext(DbContextOptions<StatisticsDbContext> options)
            : base(options)
        {
        }

    
        public DbSet<Statistic> Statistics { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
