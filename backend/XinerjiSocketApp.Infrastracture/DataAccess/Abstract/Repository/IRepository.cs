using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using XinerjiSocketApp.Infrastructure.DataAccess.Dapper.ExpressionBuilders;

namespace XinerjiSocketApp.Infrastructure.DataAccess.Abstract.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Query();

        (string, WherePart) GetSqlQuery(Expression<Func<TEntity, bool>> predict);

        IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> predict);

        Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> predict);
        Task<IEnumerable<TEntity>> QueryAsync();

        IEnumerable<TEntity> ExecuteQuery(string sql, Dictionary<string, object> parameters = null);

        public int ExecuteSql(string sql);

        public IEnumerable<dynamic> Query(string sql, Dictionary<string, object> parameters = null);

        public void Add(TEntity entity);

        Task<int> Create(TEntity entity);

        public void Modify(TEntity entity);

        public void Remove(TEntity entity);

        void Commit();

        public void Rollback();

        public TEntity Find(int id);
    }
}
