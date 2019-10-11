using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quake.Repository.QuakeSqlParameter
{
    public class SqlParam
    {
        public object Value { get; set; }
        public ParameterDirection Direction { get; set; }
        public SqlDbType SqlType { get; set; }
    }
}
