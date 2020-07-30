using MassTransit;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Zora.Notifications.Hubs;
using Zora.Shared.Messages.Students;

namespace Zora.Notifications.Messages
{
    using static Constants;

    public class NewStundentConsumer : IConsumer<StudentMessage>
    {
        private readonly IHubContext<NotificationsHub> _hub;

        public NewStundentConsumer(IHubContext<NotificationsHub> hub)
        {
            _hub = hub;
        }
        public async Task Consume(ConsumeContext<StudentMessage> context)
        {
            await _hub
              .Clients.All.SendAsync(ReceiveNotificationEndpoint, context.Message);
        }

    }
}
