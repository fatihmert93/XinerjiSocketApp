using System;
using System.Collections.Generic;
using System.Text;
using XinerjiSocketApp.Infrastructure.DataAccess.TableCreators;

namespace XinerjiSocketApp.Infrastructure.DataAccess.ScriptGenerator
{
    public class MsSqlScriptGenerator : ScriptGeneratorBase
    {
        public MsSqlScriptGenerator(Type t) : base(t)
        {
        }

        protected override Dictionary<Type, string> DataMapper()
        {
            Dictionary<Type, String> dataMapper = new Dictionary<Type, string>();
            dataMapper.Add(typeof(int), "INT");
            dataMapper.Add(typeof(int?), "INT NULL");
            dataMapper.Add(typeof(string), "NVARCHAR(MAX)");
            dataMapper.Add(typeof(bool), "BIT");
            dataMapper.Add(typeof(bool?), "BIT NULL");
            dataMapper.Add(typeof(DateTime), "DATE");
            dataMapper.Add(typeof(float), "FLOAT");
            dataMapper.Add(typeof(double), "FLOAT");
            dataMapper.Add(typeof(decimal), "DECIMAL(18,0)");
            dataMapper.Add(typeof(Guid), "UNIQUEIDENTIFIER");
            return dataMapper;
        }

        public static Dictionary<Type, string> StaticDataMapper()
        {
            Dictionary<Type, String> dataMapper = new Dictionary<Type, string>();
            dataMapper.Add(typeof(int), "INT not null");
            dataMapper.Add(typeof(int?), "INT null");
            dataMapper.Add(typeof(string), "NVARCHAR(MAX)");
            dataMapper.Add(typeof(bool), "BIT not null");
            dataMapper.Add(typeof(bool?), "BIT null");
            dataMapper.Add(typeof(DateTime), "DATE not null");
            dataMapper.Add(typeof(DateTime?), "DATE null");
            dataMapper.Add(typeof(float), "FLOAT not null");
            dataMapper.Add(typeof(float?), "FLOAT null");
            dataMapper.Add(typeof(decimal), "DECIMAL not null");
            dataMapper.Add(typeof(decimal?), "DECIMAL null");
            dataMapper.Add(typeof(long), "BIGINT not null");
            dataMapper.Add(typeof(long?), "BIGINT null");
            dataMapper.Add(typeof(byte[]), "bytea");
            dataMapper.Add(typeof(TimeSpan), "interval not null");
            dataMapper.Add(typeof(TimeSpan?), "interval null");
            dataMapper.Add(typeof(Guid), "UNIQUEIDENTIFIER not null");
            dataMapper.Add(typeof(Guid?), "UNIQUEIDENTIFIER null");
            return dataMapper;
        }

        public static Dictionary<Type, DbColumnType> DataTypeMapper()
        {
            Dictionary<Type, DbColumnType> dataMapper = new Dictionary<Type, DbColumnType>();
            dataMapper.Add(typeof(int), new DbColumnType("INT", false));
            dataMapper.Add(typeof(int?), new DbColumnType("INT", true));
            dataMapper.Add(typeof(string), new DbColumnType("NVARCHAR(MAX)", true));
            dataMapper.Add(typeof(bool), new DbColumnType("BIT", false));
            dataMapper.Add(typeof(bool?), new DbColumnType("BIT", true));
            dataMapper.Add(typeof(DateTime), new DbColumnType("DATE", false));
            dataMapper.Add(typeof(DateTime?), new DbColumnType("DATE", true));
            dataMapper.Add(typeof(float), new DbColumnType("FLOAT", false));
            dataMapper.Add(typeof(float?), new DbColumnType("FLOAT", true));
            dataMapper.Add(typeof(decimal), new DbColumnType("DECIMAL", false));
            dataMapper.Add(typeof(decimal?), new DbColumnType("DECIMAL", true));
            dataMapper.Add(typeof(long), new DbColumnType("BIGINT", false));
            dataMapper.Add(typeof(long?), new DbColumnType("BIGINT", true));
            dataMapper.Add(typeof(byte[]), new DbColumnType("bytea", false));
            dataMapper.Add(typeof(TimeSpan), new DbColumnType("interval", false));
            dataMapper.Add(typeof(TimeSpan?), new DbColumnType("interval", true));
            dataMapper.Add(typeof(Guid), new DbColumnType("UNIQUEIDENTIFIER", false));
            dataMapper.Add(typeof(Guid?), new DbColumnType("UNIQUEIDENTIFIER", true));
            return dataMapper;
        }


        public override string CreateGetTableColumnsScript()
        {
            string script = @"SELECT column_name,data_type,is_nullable
            FROM information_schema.columns
            WHERE table_schema = 'dbo'
            AND table_name = '" + this.ClassName + "'; ";

            return script;
        }
    }
}
