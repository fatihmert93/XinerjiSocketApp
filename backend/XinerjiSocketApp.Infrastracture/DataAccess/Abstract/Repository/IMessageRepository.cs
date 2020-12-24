using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XinerjiSocketApp.Model.Entities;

namespace XinerjiSocketApp.Infrastructure.DataAccess.Abstract.Repository
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task CreateMessage(Message message);
    }
}
