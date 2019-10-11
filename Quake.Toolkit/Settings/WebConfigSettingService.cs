using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quake.Toolkit.Helpers;

namespace Quake.Toolkit.Settings
{
    public class WebConfigSettingService : ISettingService
    {
        public T Get<T>(string key)
        {
            var obj = ConfigurationManager.AppSettings[key];
            return obj.ConvertTo<T>();
        }

        public T GetConnectionString<T>(string key)
        {
            var obj = ConfigurationManager.ConnectionStrings[key].ConnectionString;
            return obj.ConvertTo<T>();
        }
    }
}
