using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Playing.Events;

namespace NeverCompletedGame.Events
{
    public class EventHub : Hub
    {
        public Task JoinGroup(string group)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public Task LeaveGroup(string group)
        {
            if (group != null)
            {
                return Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
            }
            return Task.CompletedTask;
        }

    }
}
