// ChatHub.cs
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace galaxy_match_make.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string senderId, string recipientId, string messageContent)
        {
            // Save message to database (you should inject your database service here)
            // For example: await _messageService.SaveMessageAsync(senderId, recipientId, messageContent);
            
            await Clients.Users(new[] { senderId, recipientId })
            .SendAsync("ReceiveMessage", new {
                Id = 0, // Replace with actual DB ID if available
                SenderId = senderId,
                RecipientId = recipientId,
                MessageContent = messageContent,
                SentDate = DateTime.Now
            });
        }
        [HubMethodName("JoinChat")]
        public async Task JoinChat(string userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
        }
    }
}