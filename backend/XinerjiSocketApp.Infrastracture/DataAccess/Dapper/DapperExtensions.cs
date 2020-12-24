using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace XinerjiSocketApp.Infrastructure.DataAccess.Dapper
{
    public static class DapperExtensions
    {
        public static void Insert<T>(this IDbConnection cnn, string tableName, dynamic param, IDbTransaction dbTransaction = null)
        {
            SqlMapper.Query<T>(cnn, DynamicQuery.GetInsertQuery(tableName, param), param, dbTransaction);
        }

        public static void Update(this IDbConnection cnn, string tableName, dynamic param, IDbTransaction dbTransaction = null)
        {
            SqlMapper.Execute(cnn, DynamicQuery.GetUpdateQuery(tableName, param), param, dbTransaction);
        }

        public static IEnumerable<T> QueryImpl<T>(this IDbConnection cnn, string query, object param, CommandType? commandType)
        {
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            try
            {
                var response = commandType.HasValue
                    ? cnn.Query<T>(query, param, commandType: commandType)
                    : cnn.Query<T>(query, param);

                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                return response;
            }
            catch (Exception e)
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                Console.WriteLine(e);
                throw;
            }
        }

        public static IEnumerable<T> QueryImpl<T>(this IDbConnection cnn, string query, CommandType? commandType)
        {
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            try
            {
                var response = commandType.HasValue
                    ? cnn.Query<T>(query, commandType: commandType)
                    : cnn.Query<T>(query);
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                return response;
            }
            catch (Exception e)
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                    cnn.Dispose();
                }

                Console.WriteLine(e);
                throw;
            }
        }

        public static IEnumerable<T> QueryImpl<T>(this IDbConnection cnn, string query, object param)
        {
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            try
            {
                var response = cnn.Query<T>(query, param);
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                return response;
            }
            catch (Exception e)
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                Console.WriteLine(e);
                throw;
            }
        }

        public static IEnumerable<T> QueryImpl<T>(this IDbConnection cnn, string query)
        {
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            try
            {
                var response = cnn.Query<T>(query);
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                return response;
            }
            catch (Exception e)
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                Console.WriteLine(e);
                throw;
            }
        }

        public static T ExecuteScalarImpl<T>(this IDbConnection cnn, string query)
        {
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            try
            {
                var response = cnn.ExecuteScalar<T>(query);
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                return response;
            }
            catch (Exception e)
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                Console.WriteLine(e);
                throw;
            }
        }
        public static T ExecuteScalarImpl<T>(this IDbConnection cnn, string query, object param)
        {
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            try
            {
                var response = cnn.ExecuteScalar<T>(query, param);
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                return response;
            }
            catch (Exception e)
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                Console.WriteLine(e);
                throw;
            }
        }

        public static T ExecuteScalarImpl<T>(this IDbConnection cnn, string query, object param, CommandType? commandType)
        {
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            try
            {
                var response = commandType.HasValue ? cnn.ExecuteScalar<T>(query, param, commandType: commandType) :
                cnn.ExecuteScalar<T>(query, param);
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                return response;
            }
            catch (Exception e)
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                Console.WriteLine(e);
                throw;
            }
        }


        #region AsyncMethods

        public static async Task<IEnumerable<T>> QueryImplAsync<T>(this IDbConnection cnn, string query, object param, CommandType? commandType)
        {
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            try
            {
                var response = commandType.HasValue
                    ? await cnn.QueryAsync<T>(query, param, commandType: commandType)
                    : await cnn.QueryAsync<T>(query, param);

                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                return response;
            }
            catch (Exception e)
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                Console.WriteLine(e);
                throw;
            }
        }

        public static async Task<IEnumerable<T>> QueryImplAsync<T>(this IDbConnection cnn, string query, CommandType? commandType)
        {
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            try
            {
                var response = commandType.HasValue
                    ? await cnn.QueryAsync<T>(query, commandType: commandType)
                    : await cnn.QueryAsync<T>(query);
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                return response;
            }
            catch (Exception e)
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                    cnn.Dispose();
                }

                Console.WriteLine(e);
                throw;
            }
        }

        public static async Task<IEnumerable<T>> QueryImplAsync<T>(this IDbConnection cnn, string query, object param)
        {
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            try
            {
                var response = await cnn.QueryAsync<T>(query, param);
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                return response;
            }
            catch (Exception e)
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                Console.WriteLine(e);
                throw;
            }
        }

        public static async Task<IEnumerable<T>> QueryImplAsync<T>(this IDbConnection cnn, string query)
        {
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            try
            {
                var response = await cnn.QueryAsync<T>(query);
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                return response;
            }
            catch (Exception e)
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                Console.WriteLine(e);
                throw;
            }
        }

        public static async Task<T> ExecuteScalarImplAsync<T>(this IDbConnection cnn, string query)
        {
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            try
            {
                var response = await cnn.ExecuteScalarAsync<T>(query);
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                return response;
            }
            catch (Exception e)
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                Console.WriteLine(e);
                throw;
            }
        }
        public static async Task<T> ExecuteScalarImplAsync<T>(this IDbConnection cnn, string query, object param)
        {
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            try
            {
                var response = await cnn.ExecuteScalarAsync<T>(query, param);
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                return response;
            }
            catch (Exception e)
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                Console.WriteLine(e);
                throw;
            }
        }

        public static async Task<T> ExecuteScalarImplAsync<T>(this IDbConnection cnn, string query, object param, CommandType? commandType)
        {
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            try
            {
                var response = commandType.HasValue ? await cnn.ExecuteScalarAsync<T>(query, param, commandType: commandType) :
                await cnn.ExecuteScalarAsync<T>(query, param);
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                return response;
            }
            catch (Exception e)
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                Console.WriteLine(e);
                throw;
            }
        }

        #endregion

    }
}
