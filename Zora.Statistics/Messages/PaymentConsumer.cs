using MassTransit;
using System.Threading.Tasks;
using Zora.Shared.Messages.Payments;
using Zora.Statistics.Services.Statistics;

namespace Zora.Statistics.Messages
{


    public class PaymentConsumer : IConsumer<PaymentAddedMessage>
    {
        private readonly IStatisticsService statistics;

        public PaymentConsumer(IStatisticsService statistics)
            => this.statistics = statistics;

        public async Task Consume(ConsumeContext<PaymentAddedMessage> context)
        {
            await this.statistics.IncreaseAmountTotal(context.Message.Amount);
        }
    }
}
