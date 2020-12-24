using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using XinerjiSocketApp.Infrastructure.Abstract;
using XinerjiSocketApp.Infrastructure.IOC;
using XinerjiSocketApp.Model.Entities;

namespace XinerjiSocketApp.Infrastructure.Hubs
{
    public class UserChatHub : Hub
    {
        public static List<User> UserList = new List<User>();

        private readonly IMessageService _messageService;
        
        public UserChatHub()
        {
            _messageService = ServiceLocator.Current.GetInstance<IMessageService>();
        }

        public override Task OnConnectedAsync()
        {

            var connectionId = Context.ConnectionId;

            User user = new User()
            {
                Username = "",
                ConnectionId = connectionId
            };
            
            UserList.Add(user);

            Clients.All.SendAsync("GetAllUsers", UserList);

            return base.OnConnectedAsync();
        }


        public override Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;

            var user = UserList.Find(v => v.ConnectionId == connectionId);

            UserList.Remove(user);

            return base.OnDisconnectedAsync(exception);
        }


        public async Task Connect(string username)
        {
            string connectionId = Context.ConnectionId;

            var user = UserList.FirstOrDefault(v => v.ConnectionId == connectionId);
            if (user != null)
            {
                UserList.Remove(user);
                user.Username = username;
                UserList.Add(user);

                await Clients.All.SendAsync("GetAllUsers", UserList);
            }

        }

        public async Task SendGroupMessage(string message)
        {
            string connectionId = Context.ConnectionId;

            await _messageService.SendGroupMessage(connectionId, message);

            var messages = await _messageService.GetAllMessages();

            await Clients.All.SendAsync("ReceiveGroupMessage", messages);
        }


        public async Task ReceiveGroupMessage()
        {
            var messages = await _messageService.GetAllMessages();

            await Clients.All.SendAsync("ReceiveGroupMessage", messages);
        }
    }
}
