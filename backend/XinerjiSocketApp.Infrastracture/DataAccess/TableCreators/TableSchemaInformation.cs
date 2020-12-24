using System;
using System.Collections.Generic;
using System.Text;

namespace XinerjiSocketApp.Infrastructure.DataAccess.TableCreators
{
    public class TableSchemaInformation
    {
        public string column_name { get; set; }
        public string data_type { get; set; }
        public string is_nullable { get; set; }
    }
}
