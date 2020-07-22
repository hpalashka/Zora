using Refit;
using System.Threading.Tasks;
using Zora.Web.Models.Statistics.ViewModels;

namespace Zora.Web.Services.Statistics
{
    public interface IStatisticsService
    {
        [Get("/Statistics")]
        Task<StatisticsViewModel> Totals();
    }
}
