using System;
using System.Collections.Generic;
using System.Text;

namespace XinerjiSocketApp.Infrastructure.DataAccess.TableCreators
{
    public interface ITableCreator
    {
        bool IsTableExists(string tableName);
        void CreateTable(Type type);
        void CreateAllTable<TImplement>();
    }
}
