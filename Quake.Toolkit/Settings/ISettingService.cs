using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quake.Toolkit.Settings
{
    public interface ISettingService
    {
        T Get<T>(string key);
        T GetConnectionString<T>(string key);
    }
}
