using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using XinerjiSocketApp.Infrastructure.Abstract;
using XinerjiSocketApp.Infrastructure.DataAccess.Abstract.Repository;
using XinerjiSocketApp.Infrastructure.Hubs;

namespace XinerjiSocketApp.Service
{
    public class UserService : IUserService
    {
        private readonly IHubContext<UserChatHub> _userHubContext;
        private readonly IUserRepository _userRepository;

        public UserService(IHubContext<UserChatHub> userHubContext, IUserRepository userRepository)
        {
            _userHubContext = userHubContext;
            _userRepository = userRepository;
        }

        public async Task Login(string username, string password)
        {
            var user = (await _userRepository
                .QueryAsync(v => v.Username == username))
                .FirstOrDefault();
            
            
            if (user != null)
            {
                if (user.Password == password)
                {
                    await _userHubContext.Clients.All.SendAsync("ReceiveUser", user);
                }
            }
        }
    }
}
