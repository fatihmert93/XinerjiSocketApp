using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace XinerjiSocketApp.Infrastructure.DataAccess.Dapper.MsSql
{
    public class SqlConnectionFactory : ConnectionFactoryBase
    {
        protected override DbProviderFactory GetProviderFactory()
        {
            var factory = DbProviderFactories.GetDbProviderFactory(DataAccessProviderTypes.SqlServer);
            return factory;
        }

        public SqlConnectionFactory(string connectionString) : base(connectionString)
        {
        }
    }
}
