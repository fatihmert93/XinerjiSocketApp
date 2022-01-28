using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace XinerjiSocketApp.Infrastructure.DataAccess.Dapper.MsSql
{
    public class PostgreConnectionFactory : ConnectionFactoryBase
    {
        public PostgreConnectionFactory(string connectionString) : base(connectionString)
        {
        }

        protected override DbProviderFactory GetProviderFactory()
        {
            var factory = DbProviderFactories.GetDbProviderFactory(DataAccessProviderTypes.PostgreSql);
            return factory;
        }
    }
}
