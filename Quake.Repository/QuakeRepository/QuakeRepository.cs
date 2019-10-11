using Quake.Repository.QaukeContext;
using Quake.Repository.QuakeSqlParameter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Quake.Repository.QuakeRepository
{
    public class QuakeRepository : IQuakeRepository
    {
        public IQuakeContext Context { get; }
        public QuakeRepository(IQuakeContext context)
        {
            Context = context;
        }

        public Tuple<IEnumerable<T1>> QueryWithMultiOutput<T1>(string query, Dictionary<string, SqlParam> parameters = null)
        {

            Dapper.SqlMapper.GridReader reader = null;
            var _connection = Context.Connection;

            if (parameters == null)
            {
                using (var result = _connection.QueryMultiple(query, null, null, null, CommandType.StoredProcedure))
                {
                    reader = result;
                    var data1 = reader.Read<T1>().ToList();
                    _connection.Close();
                    return new Tuple<IEnumerable<T1>>(data1);
                }
            }
            else
            {
                var ps = new SqlDynamicParameters();
                foreach (var p in parameters)
                {
                    ps.Add(p.Key, p.Value.Value, p.Value.SqlType, p.Value.Direction);
                }
                using (var result = _connection.QueryMultiple(query, ps, null, 1000000, CommandType.StoredProcedure))
                {

                    var data1 = result.Read<T1>().ToList();
                    _connection.Close();
                    return new Tuple<IEnumerable<T1>>(data1);
                }

            }

        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>> QueryWithMultiOutput<T1, T2>(string query, Dictionary<string, SqlParam> parameters = null)
        {

            Dapper.SqlMapper.GridReader reader = null;
            var _connection = Context.Connection;

            if (parameters == null)
            {
                using (var result = _connection.QueryMultiple(query, null, null, null, CommandType.StoredProcedure))
                {
                    reader = result;
                    var data1 = reader.Read<T1>().ToList();
                    var data2 = reader.Read<T2>().ToList();
                    _connection.Close();
                    return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(data1, data2);
                }
            }
            else
            {
                var ps = new SqlDynamicParameters();
                foreach (var p in parameters)
                {
                    ps.Add(p.Key, p.Value.Value, p.Value.SqlType, p.Value.Direction);
                }
                using (var result = _connection.QueryMultiple(query, ps, null, 1000000, CommandType.StoredProcedure))
                {

                    var data1 = result.Read<T1>().ToList();
                    var data2 = result.Read<T2>().ToList();
                    _connection.Close();
                    return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(data1, data2);
                }
            }

        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> QueryWithMultiOutput<T1, T2, T3>(string query, Dictionary<string, SqlParam> parameters = null)
        {

            Dapper.SqlMapper.GridReader reader = null;
            var _connection = Context.Connection;

            if (parameters == null)
            {
                using (var result = _connection.QueryMultiple(query, null, null, null, CommandType.StoredProcedure))
                {
                    reader = result;
                    var data1 = reader.Read<T1>().ToList();
                    var data2 = reader.Read<T2>().ToList();
                    var data3 = reader.Read<T3>().ToList();
                    _connection.Close();
                    return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>(data1, data2, data3);
                }
            }
            else
            {
                var ps = new SqlDynamicParameters();
                foreach (var p in parameters)
                {
                    ps.Add(p.Key, p.Value.Value, p.Value.SqlType, p.Value.Direction);
                }
                using (var result = _connection.QueryMultiple(query, ps, null, 1000000, CommandType.StoredProcedure))
                {

                    var data1 = result.Read<T1>().ToList();
                    var data2 = result.Read<T2>().ToList();
                    var data3 = result.Read<T3>().ToList();
                    _connection.Close();
                    return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>(data1, data2, data3);
                }
            }

        }
        public Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>> QueryWithMultiOutput<T1, T2, T3, T4>(string query, Dictionary<string, SqlParam> parameters = null)
        {

            Dapper.SqlMapper.GridReader reader = null;
            var _connection = Context.Connection;

            if (parameters == null)
            {
                using (var result = _connection.QueryMultiple(query, null, null, null, CommandType.StoredProcedure))
                {
                    reader = result;
                    var data1 = reader.Read<T1>().ToList();
                    var data2 = reader.Read<T2>().ToList();
                    var data3 = reader.Read<T3>().ToList();
                    var data4 = reader.Read<T4>().ToList();
                    _connection.Close();
                    return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>>(data1, data2, data3, data4);
                }
            }
            else
            {
                var ps = new SqlDynamicParameters();
                foreach (var p in parameters)
                {
                    ps.Add(p.Key, p.Value.Value, p.Value.SqlType, p.Value.Direction);
                }
                using (var result = _connection.QueryMultiple(query, ps, null, 1000000, CommandType.StoredProcedure))
                {

                    var data1 = result.Read<T1>().ToList();
                    var data2 = result.Read<T2>().ToList();
                    var data3 = result.Read<T3>().ToList();
                    var data4 = result.Read<T4>().ToList();
                    _connection.Close();
                    return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>>(data1, data2, data3, data4);
                }
            }

        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>> QueryWithMultiOutput<T1, T2, T3, T4, T5>(string query, Dictionary<string, SqlParam> parameters = null)
        {
            Dapper.SqlMapper.GridReader reader = null;
            var _connection = Context.Connection;

            if (parameters == null)
            {
                using (var result = _connection.QueryMultiple(query, null, null, null, CommandType.StoredProcedure))
                {
                    reader = result;
                    var data1 = reader.Read<T1>().ToList();
                    var data2 = reader.Read<T2>().ToList();
                    var data3 = reader.Read<T3>().ToList();
                    var data4 = reader.Read<T4>().ToList();
                    var data5 = reader.Read<T5>().ToList();
                    _connection.Close();
                    return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>>(data1, data2, data3, data4, data5);
                }
            }
            else
            {
                var ps = new SqlDynamicParameters();
                foreach (var p in parameters)
                {
                    ps.Add(p.Key, p.Value.Value, p.Value.SqlType, p.Value.Direction);
                }
                using (var result = _connection.QueryMultiple(query, ps, null, 1000000, CommandType.StoredProcedure))
                {

                    var data1 = result.Read<T1>().ToList();
                    var data2 = result.Read<T2>().ToList();
                    var data3 = result.Read<T3>().ToList();
                    var data4 = result.Read<T4>().ToList();
                    var data5 = result.Read<T5>().ToList();
                    _connection.Close();
                    return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>>(data1, data2, data3, data4, data5);
                }
            }
        }
        public Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>> QueryWithMultiOutput<T1, T2, T3, T4, T5, T6>(string query, Dictionary<string, SqlParam> parameters = null)
        {
            Dapper.SqlMapper.GridReader reader = null;
            var _connection = Context.Connection;

            if (parameters == null)
            {
                using (var result = _connection.QueryMultiple(query, null, null, null, CommandType.StoredProcedure))
                {
                    reader = result;
                    var data1 = reader.Read<T1>().ToList();
                    var data2 = reader.Read<T2>().ToList();
                    var data3 = reader.Read<T3>().ToList();
                    var data4 = reader.Read<T4>().ToList();
                    var data5 = reader.Read<T5>().ToList();
                    var data6 = reader.Read<T6>().ToList();
                    _connection.Close();
                    return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>>(data1, data2, data3, data4, data5, data6);
                }
            }
            else
            {
                var ps = new SqlDynamicParameters();
                foreach (var p in parameters)
                {
                    ps.Add(p.Key, p.Value.Value, p.Value.SqlType, p.Value.Direction);
                }
                using (var result = _connection.QueryMultiple(query, ps, null, 1000000, CommandType.StoredProcedure))
                {

                    var data1 = result.Read<T1>().ToList();
                    var data2 = result.Read<T2>().ToList();
                    var data3 = result.Read<T3>().ToList();
                    var data4 = result.Read<T4>().ToList();
                    var data5 = result.Read<T5>().ToList();
                    var data6 = result.Read<T6>().ToList();
                    _connection.Close();
                    return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>>(data1, data2, data3, data4, data5, data6);
                }
            }

        }

        public int ExecuteNonQuery(string query, Dictionary<string, SqlParam> parameters = null)
        {
            int result;
            SqlDynamicParameters ps = null;
            if (parameters != null && parameters.Any())
            {
                ps = new SqlDynamicParameters();
                foreach (var p in parameters)
                {
                    ps.Add(p.Key, p.Value.Value, p.Value.SqlType, p.Value.Direction);
                }
            }
            var _connection = Context.Connection;
            result = _connection.Execute(query, ps, null, null, CommandType.StoredProcedure);
            _connection.Close();
            return result;
        }
    }
}
