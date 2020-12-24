using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using XinerjiSocketApp.Infrastructure.DataAccess.Abstract.Repository;
using XinerjiSocketApp.Model.Entities;

namespace XinerjiSocketApp.Infrastructure.DataAccess.Dapper.Repositories
{
    public class MessageRepository : DapperGenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task CreateMessage(Message message)
        {

            DynamicParameters paramDistribution = new DynamicParameters();
            paramDistribution.Add("@SenderId", message.SenderId, DbType.Int32);
            paramDistribution.Add("@ReceiverId", (int)message.ReceiverId, DbType.Int32);
            paramDistribution.Add("@CreateDate", message.CreateDate, DbType.DateTime);
            paramDistribution.Add("@SenderUsername",message.SenderUsername);
            paramDistribution.Add("ReceiverUsername",message.ReceiverUsername);
            paramDistribution.Add("MessageContent",message.MessageContent);

            await ConnectionFactory.Connection.QueryImplAsync<int>(
                "INSERT Message (SenderId, ReceiverId,SenderUsername, ReceiverUsername, MessageContent, CreateDate) VALUES(@SenderId,@ReceiverId,@SenderUsername,@ReceiverUsername,@MessageContent,@CreateDate)", paramDistribution);
        }
    }
}
