using System;
using System.Collections.Generic;
using System.Text;
using XinerjiSocketApp.Infrastructure.DataAccess.Abstract.Repository;
using XinerjiSocketApp.Model.Entities;

namespace XinerjiSocketApp.Infrastructure.DataAccess.Dapper.Repositories
{
    public class MessageRepository : DapperGenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}
