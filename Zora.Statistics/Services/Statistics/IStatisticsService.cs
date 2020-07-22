using System.Threading.Tasks;
using Zora.Statistics.Models.Statistics;

namespace Zora.Statistics.Services.Statistics
{
    public interface IStatisticsService
    {
        Task<StatisticsViewModel> Totals();

        Task IncreaseAmountTotal(double amount);

        Task IncreasePaidAmountTotal(double amount);

        Task IncreaseStudentsCount();
    }
}
