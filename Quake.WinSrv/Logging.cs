using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quake.WinSrv
{
    public class Logging
    {
        public static void Save(string name, string v, DateTime time)
        {
            SqlConnection con = new SqlConnection("Server=quakedb.database.windows.net;Database=quakedb;User Id=quakedbuser;Password=!2018Quake2018!;");
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_PostLog"; // This will be the stored procedures name
            SqlParameter param1 = new SqlParameter("@Name", name);
            SqlParameter param2 = new SqlParameter("@V", v);
            SqlParameter param3 = new SqlParameter("@Time", DateTime.Now);
            cmd.Parameters.Add(param1);
            cmd.Parameters.Add(param2);
            cmd.Parameters.Add(param3);
            cmd.ExecuteNonQuery();
        }
    }
}