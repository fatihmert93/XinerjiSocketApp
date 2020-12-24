using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace XinerjiSocketApp.Infrastructure.DataAccess.Dapper
{
    public abstract class ConnectionFactoryBase : IConnectionFactory
    {
        private string _connectionString;

        protected abstract DbProviderFactory GetProviderFactory();

        protected ConnectionFactoryBase(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected virtual string GetConnectionString()
        {
            return _connectionString;
        }

        public void SetConnectionString(string connectionString)
        {
            this._connectionString = connectionString;
        }


        public IDbConnection Connection
        {
            get
            {
                var factory = GetProviderFactory();

                var connection = factory.CreateConnection();
                if (connection == null) throw new Exception("There is available connection factory!");
                connection.ConnectionString = GetConnectionString();
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                return connection;
            }
        }

        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            if (disposing)
            {
                Connection.Close();
            }


            _disposedValue = true;
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ConnectionFactory() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
