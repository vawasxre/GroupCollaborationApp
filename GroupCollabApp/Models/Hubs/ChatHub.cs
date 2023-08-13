using GroupCollabApp.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GroupCollabApp.Models.Hubs
{
    public class ChatHub : Hub
    {
        private readonly GroupCollabAppAuthDbContext _context;

        public ChatHub(GroupCollabAppAuthDbContext context)
        {
            _context = context;
        }


        public async Task SendMessage(string message)
        {
            var userName = Context.User.Identity.Name;
            var chatMessage = new ChatMessage
            {
                UserName = userName,
                Message = message,
                Timestamp = DateTime.UtcNow
            };

            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();

            await Clients.All.SendAsync("ReceiveMessage", userName, message);

            await Clients.Caller.SendAsync("ClearMessageInput");
        }
        
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            var chatMessages = await _context.ChatMessages
                .OrderByDescending(m => m.Timestamp)
                .Take(20)
                .ToListAsync();
            chatMessages.Reverse();
            foreach (var chatMessage in chatMessages)
            {
                await Clients.Caller.SendAsync("ReceiveMessage", chatMessage.UserName, chatMessage.Message);
            }
        }
    }

}
