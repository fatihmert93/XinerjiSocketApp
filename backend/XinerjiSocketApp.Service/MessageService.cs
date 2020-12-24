using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using XinerjiSocketApp.Infrastructure.Abstract;
using XinerjiSocketApp.Infrastructure.DataAccess.Abstract.Repository;
using XinerjiSocketApp.Infrastructure.Hubs;
using XinerjiSocketApp.Model.Entities;

namespace XinerjiSocketApp.Service
{
    public class MessageService : IMessageService
    {
        private readonly IHubContext<UserChatHub> _userChatHubContext;
        private readonly IMessageRepository _messageRepository;

        public MessageService(IHubContext<UserChatHub> userChatHubContext, IMessageRepository messageRepository)
        {
            _userChatHubContext = userChatHubContext;
            _messageRepository = messageRepository;
        }

        public async Task SendMessage(string senderConnectionId, string receiverConnectionId, string message)
        {
            var receiverUser = UserChatHub.UserList.FirstOrDefault(v => v.ConnectionId == receiverConnectionId);

            var senderUser = UserChatHub.UserList.FirstOrDefault(v => v.ConnectionId == senderConnectionId);

            if (receiverUser != null && senderUser != null)
            {

                Message messageObj = new Message
                {
                    CreateDate = DateTime.Now,
                    MessageContent = message,
                    ReceiverUsername = receiverUser.Username,
                    ReceiverId = receiverUser.Id,
                    SenderUsername = senderUser.Username,
                    SenderId = senderUser.Id
                };

                await _userChatHubContext.Clients.Client(receiverConnectionId).SendAsync("ReceiveMessage", messageObj);

                await _messageRepository.CreateMessage(messageObj);
            }

        }

        public async Task SendGroupMessage(string senderConnectionId, string message)
        {

            var senderUser = UserChatHub.UserList.FirstOrDefault(v => v.ConnectionId == senderConnectionId);

            if (senderUser != null)
            {

                Message messageObj = new Message
                {
                    CreateDate = DateTime.Now,
                    MessageContent = message,
                    SenderUsername = senderUser.Username,
                    SenderId = senderUser.Id
                };

                await _messageRepository.CreateMessage(messageObj);

                var allMessages = (await _messageRepository.QueryAsync())
                    .OrderByDescending(v => v.CreateDate).ToList();

                await _userChatHubContext.Clients.All.SendAsync("ReceiveGroupMessage", allMessages);

                
            }

        }

        public async Task<IEnumerable<Message>> GetAllMessageByReceiverId(int receiverUserId)
        {
            var messages = await _messageRepository
                .QueryAsync(v => v.ReceiverId == receiverUserId);
            return messages;
        }

        public async Task<IEnumerable<Message>> GetAllMessages()
        {
            var allMessages = (await _messageRepository.QueryAsync())
                .OrderByDescending(v => v.CreateDate).ToList();
            return allMessages;
        }
    }
}
