﻿using Microsoft.AspNetCore.SignalR;

namespace Markis.Infrastructure.Hubs
{
    public class CommentHub : Hub
    {
        public async Task SendComment(string user, string message, int postId)
        {
            await Clients.All.SendAsync("ReceiveComment", user, message, postId);
        }
    }
}