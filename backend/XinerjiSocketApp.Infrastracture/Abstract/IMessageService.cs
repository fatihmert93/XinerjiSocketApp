using System.Collections.Generic;
using System.Threading.Tasks;
using XinerjiSocketApp.Model.Entities;

namespace XinerjiSocketApp.Infrastructure.Abstract
{
    public interface IMessageService
    {
        Task SendMessage(string senderConnectionId, string receiverConnectionId, string message);
        Task SendGroupMessage(string senderConnectionId, string message);

        Task<IEnumerable<Message>> GetAllMessageByReceiverId(int receiverUserId);
        Task<IEnumerable<Message>> GetAllMessages();

    }
}
