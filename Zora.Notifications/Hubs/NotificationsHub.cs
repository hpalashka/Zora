﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Zora.Notifications.Hubs
{
    using static Constants;

    public class NotificationsHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            if (this.Context.User.Identity.IsAuthenticated)
            {
                await this.Groups.AddToGroupAsync(
                    this.Context.ConnectionId, 
                    AuthenticatedUsersGroup);
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (this.Context.User.Identity.IsAuthenticated)
            {
                await this.Groups.RemoveFromGroupAsync(
                    this.Context.ConnectionId,
                    AuthenticatedUsersGroup);
            }
        }
    }
}
