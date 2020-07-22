using MassTransit;
using System.Threading.Tasks;
using Zora.Shared.Messages.Students;
using Zora.Statistics.Services.Statistics;

namespace Zora.Statistics.Messages
{

    public class StudentConsumer : IConsumer<StudentMessage>
    {
        private readonly IStatisticsService statistics;

        public StudentConsumer(IStatisticsService statistics)
            => this.statistics = statistics;

        public async Task Consume(ConsumeContext<StudentMessage> context)
        {
            await this.statistics.IncreaseStudentsCount();
        }
    }
}
