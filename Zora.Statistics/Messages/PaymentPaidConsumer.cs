using MassTransit;
using System.Threading.Tasks;
using Zora.Shared.Messages.Payments;
using Zora.Statistics.Services.Statistics;

namespace Zora.Statistics.Messages
{


    public class PaymentPaidConsumer : IConsumer<PaymentPaiddMessage>
    {
        private readonly IStatisticsService statistics;

        public PaymentPaidConsumer(IStatisticsService statistics)
            => this.statistics = statistics;

        public async Task Consume(ConsumeContext<PaymentPaiddMessage> context)
        {
            await this.statistics.IncreasePaidAmountTotal(context.Message.Amount);
        }
    }
}
