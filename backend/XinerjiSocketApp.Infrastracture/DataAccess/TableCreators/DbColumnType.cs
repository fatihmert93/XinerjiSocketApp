using System;
using System.Collections.Generic;
using System.Text;

namespace XinerjiSocketApp.Infrastructure.DataAccess.TableCreators
{
    public class DbColumnType
    {
        public DbColumnType(string type, bool isNullable)
        {
            Type = type;
            IsNullable = isNullable;
        }


        public string Type { get; set; }
        public bool IsNullable { get; set; }
    }
}
