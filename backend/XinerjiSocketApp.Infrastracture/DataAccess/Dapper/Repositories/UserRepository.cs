using System;
using System.Collections.Generic;
using System.Text;
using XinerjiSocketApp.Infrastructure.DataAccess.Abstract.Repository;
using XinerjiSocketApp.Model.Entities;

namespace XinerjiSocketApp.Infrastructure.DataAccess.Dapper.Repositories
{
    public class UserRepository : DapperGenericRepository<User>, IUserRepository
    {
        public UserRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}
