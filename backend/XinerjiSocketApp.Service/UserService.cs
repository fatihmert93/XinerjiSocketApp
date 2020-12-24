using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using XinerjiSocketApp.Infrastructure.DataAccess.Abstract.Repository;
using XinerjiSocketApp.Infrastructure.Hubs;
using XinerjiSocketApp.Service.Abstract;

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
    }
}
