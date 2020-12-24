using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using XinerjiSocketApp.Model.Entities;

namespace XinerjiSocketApp.Infrastructure.Hubs
{
    public class UserChatHub : Hub
    {
        public static List<User> UserList = new List<User>();

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
    }
}
