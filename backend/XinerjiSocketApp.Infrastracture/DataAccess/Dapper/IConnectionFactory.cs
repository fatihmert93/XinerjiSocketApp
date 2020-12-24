using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace XinerjiSocketApp.Infrastructure.DataAccess.Dapper
{
    public interface IConnectionFactory : IDisposable
    {
        IDbConnection Connection { get; }
    }
}
