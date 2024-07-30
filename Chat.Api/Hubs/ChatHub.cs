using Chat.Application.Abstractions;
using Chat.Domain.Dtos;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Azure.SignalR.Management;

namespace Chat.Api.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IServiceManager serviceManager;
        private readonly IDictionary<string, string> connectedUsers;
        private readonly IMessageService messageService;
        private readonly IUserService userService;
        private readonly IAnalysisService analysisService;

        public ChatHub(
            IDictionary<string, string> connectedUsers,
            IMessageService messageService,
            IUserService userService
            )
        {
            this.connectedUsers = connectedUsers;
            this.messageService = messageService;
            this.userService = userService;
        }

        public async Task OnJoin(string userId)
        {
            var connectionId = Context.ConnectionId;
            if (connectedUsers.TryAdd(connectionId, userId))
            {
                await GetConnectedUsers();
                var loadedMessages = await messageService.LoadMessages();
                await Clients.Caller.SendAsync("LoadMessages", loadedMessages);

                var user = await userService.GetUserById(Guid.Parse(userId));
        
                var botMessage =  messageService.CreateBotMessage($"{user.FirstName} {user.SecondName} join our room");

                await Clients.All.SendAsync("OnJoin", botMessage);
                
            }
        }

        public async Task SendMessage(string message)
        {
            string connectionId = Context.ConnectionId;

            if (connectedUsers.TryGetValue(connectionId, out string userId))
            {
                var userMessage = await messageService.CreateUserMessage(userId, message);

                await Clients.All.SendAsync("ReceiveMessage", userMessage);
                await messageService.SaveMessage(userMessage);
            }
        }

        
        
        public override async Task OnDisconnectedAsync(Exception ex)
        {
            string connectionId = Context.ConnectionId;
            if (connectedUsers.Remove(connectionId, out string userId))
            {
                var user = await userService.GetUserById(Guid.Parse(userId));
                var botMessage =  messageService.CreateBotMessage($"{user.FirstName} {user.SecondName} left the chat");

                await Clients.All.SendAsync("ReceiveMessage", botMessage);
                await GetConnectedUsers();

                await base.OnDisconnectedAsync(ex);
            }
        }

        public async Task GetConnectedUsers()
        {
            var userIds = connectedUsers.Values.ToList();
            var guids = new List<Guid>();
            foreach (var userId in userIds)
            {
                guids.Add(Guid.Parse(userId));
            }
            var users = await userService.GetUsersByIdsAsync(guids);
            await Clients.All.SendAsync("ReceiveConnectedUsers", users);
        }
        
    }
}
