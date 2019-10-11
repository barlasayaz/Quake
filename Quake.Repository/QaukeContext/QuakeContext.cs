using Quake.Toolkit.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quake.Repository.QaukeContext
{
    public class QuakeContext : IQuakeContext
    {
        private readonly string _connectionString;
        private IDbConnection _connection;

        private readonly ISettingService _settingService;
        public QuakeContext(ISettingService settingService)
        {
            _settingService = settingService;
            _connectionString = _settingService.GetConnectionString<string>("QuakeAzureConnectionString");
            //_connectionString = _settingService.GetConnectionString<string>("QuakeConnectionString");
        }
        public void Dispose()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
                _connection.Close();
        }
        public IDbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SqlConnection(_connectionString);
                }
                if (_connection.State != ConnectionState.Open) _connection.Open();
                return _connection;
            }
        }
    }
}
