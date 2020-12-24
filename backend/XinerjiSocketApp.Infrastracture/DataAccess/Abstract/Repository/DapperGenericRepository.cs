using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dapper;
using XinerjiSocketApp.Infrastructure.DataAccess.Dapper;
using XinerjiSocketApp.Infrastructure.DataAccess.Dapper.ExpressionBuilders;

namespace XinerjiSocketApp.Infrastructure.DataAccess.Abstract.Repository
{
    public abstract class DapperGenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly IConnectionFactory ConnectionFactory;
        private readonly string _tableName;
        private IDbTransaction _dbTransaction;


        public DapperGenericRepository(IConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory;
            var type = typeof(TEntity);
            _tableName = type.Name;
            // ReSharper disable once VirtualMemberCallInConstructor
            // ReSharper disable once VirtualMemberCallInConstructor
        }


        protected virtual dynamic InsertMapping(TEntity item)
        {
            return item;
        }

        protected virtual dynamic UpdateMapping(TEntity item)
        {
            return item;
        }

        public IEnumerable<TEntity> Query()
        {
            string query = "SELECT * FROM " + _tableName;
            return ConnectionFactory.Connection.Query<TEntity>(query);
        }

        public (string, WherePart) GetSqlQuery(Expression<Func<TEntity, bool>> predict)
        {
            WhereBuilder whereBuilder = new WhereBuilder();
            WherePart wherePart = whereBuilder.ToSql(predict);
            string whereQuery = wherePart.Sql;
            string query = $"SELECT * FROM  {_tableName} WHERE {whereQuery}";
            return (query, wherePart);
        }

        public virtual IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> predict)
        {
            WhereBuilder whereBuilder = new WhereBuilder();
            WherePart wherePart = whereBuilder.ToSql(predict);
            string whereQuery = wherePart.Sql;
            string query = $"SELECT * FROM  {_tableName} WHERE {whereQuery}";
            return ConnectionFactory.Connection.Query<TEntity>(query, wherePart.Parameters);
        }

        public async Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> predict)
        {
            WhereBuilder whereBuilder = new WhereBuilder();
            WherePart wherePart = whereBuilder.ToSql(predict);
            string whereQuery = wherePart.Sql;
            string query = $"SELECT * FROM  {_tableName} WHERE {whereQuery}";
            return (await ConnectionFactory.Connection.QueryImplAsync<TEntity>(query, wherePart.Parameters));
        }

        public async Task<IEnumerable<TEntity>> QueryAsync()
        {
            string query = $"SELECT * FROM  {_tableName}";
            return (await ConnectionFactory.Connection.QueryImplAsync<TEntity>(query));
        }

        public IEnumerable<TEntity> ExecuteQuery(string sql, Dictionary<string, object> parameters = null)
        {
            return ConnectionFactory.Connection.Query<TEntity>(sql, parameters);
        }

        public int ExecuteSql(string sql)
        {
            return 0;
        }

        public IEnumerable<dynamic> Query(string sql, Dictionary<string, object> parameters = null)
        {
            IEnumerable<dynamic> query = ConnectionFactory.Connection.Query(sql, parameters);
            return query;
        }

        public void Add(TEntity entity)
        {
            if (ConnectionFactory.Connection.State == ConnectionState.Closed)
                ConnectionFactory.Connection.Open();
            _dbTransaction = ConnectionFactory.Connection.BeginTransaction();
            var parameters = (object)InsertMapping(entity);
            ConnectionFactory.Connection.Insert<int>(_tableName, parameters, _dbTransaction);
        }

        public async Task<int> Create(TEntity entity)
        {
            if (ConnectionFactory.Connection.State == ConnectionState.Closed)
                ConnectionFactory.Connection.Open();
            var parameters = (object)InsertMapping(entity);

            string query = DynamicQuery.GetInsertQuery(_tableName, parameters);

            var result = await ConnectionFactory.Connection.ExecuteScalarAsync<int>(query, parameters);

            return result;
        }

        public void Modify(TEntity entity)
        {
            if (ConnectionFactory.Connection.State == ConnectionState.Closed)
                ConnectionFactory.Connection.Open();
            _dbTransaction = ConnectionFactory.Connection.BeginTransaction();
            var parameters = (object)UpdateMapping(entity);
            ConnectionFactory.Connection.Update(_tableName, parameters, _dbTransaction);
        }

        public void Remove(TEntity entity)
        {
            if (ConnectionFactory.Connection.State == ConnectionState.Closed)
                ConnectionFactory.Connection.Open();

            Type classType = entity.GetType();

            if (!classType.GetProperties().Any(v => v.Name.Contains("Id") || v.Name.Contains("id")))
            {
                throw new ArgumentNullException("There is no id parameter!");
            }

            string id = classType.GetProperties().FirstOrDefault(v => v.Name.Contains("Id") || v.Name.Contains("id"))?.ToString();

            _dbTransaction = ConnectionFactory.Connection.BeginTransaction();
            ConnectionFactory.Connection.Execute("DELETE FROM " + _tableName + " WHERE Id=@Id", new { ID = id }, _dbTransaction);
        }

        public void Commit()
        {
            _dbTransaction.Commit();
            ConnectionFactory.Connection.Close();
        }

        public void Rollback()
        {
            _dbTransaction.Rollback();
            ConnectionFactory.Connection.Close();
        }

        public TEntity Find(int id)
        {
            TEntity item = default(TEntity);
            item = ConnectionFactory.Connection.Query<TEntity>("SELECT * FROM " + _tableName + " WHERE Id=@Id", new { Id = id }).SingleOrDefault();
            return item;
        }


        public void Dispose()
        {
            ConnectionFactory.Connection?.Dispose();
            ConnectionFactory?.Dispose();
            _dbTransaction?.Dispose();
        }
    }
}
