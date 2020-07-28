using System.Linq;
using Zora.Shared.Services;
using Zora.Statistics.Data.Models;

namespace Zora.Statistics.Data
{
    public class StatisticsDataSeeder : IDataSeeder
    {
        private readonly StatisticsDbContext db;

        public StatisticsDataSeeder(StatisticsDbContext db) => this.db = db;

        public void SeedData()
        {
            if (this.db.Statistics.Any())
            {
                return;
            }

            this.db.Statistics.Add(new Statistic
            {
                TotalAmount = 0,
                TotalPaidAmount = 0,
                TotalStudents = 1
            });

            this.db.SaveChanges();
        }
    }
}
