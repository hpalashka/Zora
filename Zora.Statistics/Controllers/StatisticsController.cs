using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zora.Shared.Controllers;
using Zora.Statistics.Models.Statistics;
using Zora.Statistics.Services.Statistics;

namespace Zora.Statistics.Controllers
{
    public class StatisticsController : ApiController
    {
        private readonly IStatisticsService _statistics;

        public StatisticsController(IStatisticsService statistics)
            => _statistics = statistics;

        [HttpGet]
        public async Task<StatisticsViewModel> Totals()
        {
            return await _statistics.Totals();
        }
    }
}
