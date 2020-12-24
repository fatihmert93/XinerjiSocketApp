using System;
using System.Collections.Generic;
using System.Text;
using XinerjiSocketApp.Infrastructure.DataAccess.Abstract.Repository;

namespace XinerjiSocketApp.Infrastructure.DataAccess.Dapper.MsSql
{
    public class SqlGenericRepository<TEntity> : DapperGenericRepository<TEntity> where TEntity : class
    {
        protected SqlGenericRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}
