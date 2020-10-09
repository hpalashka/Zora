using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Zora.Shared.Services;
using Zora.Statistics.Data;
using Zora.Statistics.Data.Models;
using Zora.Statistics.Models.Statistics;

namespace Zora.Statistics.Services.Statistics
{

    public class StatisticsService : DataService<Statistic>, IStatisticsService
    {


        public StatisticsService(StatisticsDbContext db)
            : base(db) { }


        public async Task IncreaseAmountTotal(decimal amount)
        {
            var statistics = await this.All().SingleOrDefaultAsync();

            statistics.TotalAmount = statistics.TotalAmount + amount;

            await this.Data.SaveChangesAsync();
        }

        public async Task IncreasePaidAmountTotal(decimal amount)
        {
            var statistics = await this.All().SingleOrDefaultAsync();

            statistics.TotalPaidAmount = statistics.TotalPaidAmount + amount;

            await this.Data.SaveChangesAsync();
        }

        public async Task IncreaseStudentsCount()
        {
            var statistics = await this.All().SingleOrDefaultAsync();

            statistics.TotalStudents++;

            await this.Data.SaveChangesAsync();
        }

        public async Task<StatisticsViewModel> Totals()
        {
            return await this.All().Select(s => new StatisticsViewModel()
            {
                TotalAmount = s.TotalAmount,
                TotalPaidAmount = s.TotalPaidAmount,
                TotalStudents = s.TotalStudents

            }).SingleOrDefaultAsync();
        }
    }
}
