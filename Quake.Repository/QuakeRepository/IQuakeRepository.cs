using Quake.Repository.QuakeSqlParameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quake.Repository.QuakeRepository
{
    public interface IQuakeRepository
    {
        Tuple<IEnumerable<T1>> QueryWithMultiOutput<T1>(string query, Dictionary<string, SqlParam> parameters = null);
        Tuple<IEnumerable<T1>, IEnumerable<T2>> QueryWithMultiOutput<T1, T2>(string query, Dictionary<string, SqlParam> parameters = null);
        Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> QueryWithMultiOutput<T1, T2, T3>(string query, Dictionary<string, SqlParam> parameters = null);
        Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>> QueryWithMultiOutput<T1, T2, T3, T4>(string query, Dictionary<string, SqlParam> parameters = null);
        Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>> QueryWithMultiOutput<T1, T2, T3, T4, T5>(string query, Dictionary<string, SqlParam> parameters = null);

        Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>> QueryWithMultiOutput<T1, T2, T3, T4, T5, T6>(string query, Dictionary<string, SqlParam> parameters = null);
        int ExecuteNonQuery(string query, Dictionary<string, SqlParam> parameters = null);
    }
}
