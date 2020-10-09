using System.Threading.Tasks;
using Zora.Statistics.Models.Statistics;

namespace Zora.Statistics.Services.Statistics
{
    public interface IStatisticsService
    {
        Task<StatisticsViewModel> Totals();

        Task IncreaseAmountTotal(decimal amount);

        Task IncreasePaidAmountTotal(decimal amount);

        Task IncreaseStudentsCount();
    }
}
