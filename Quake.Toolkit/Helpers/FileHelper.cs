using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Quake.Toolkit.Helpers
{
    public static class FileHelper
    {
        public static string ConvertBase64String(Stream data)
        {

            using (MemoryStream target = new MemoryStream())
            {
                data.CopyTo(target);
                byte[] dataBytes = target.ToArray();
                return Convert.ToBase64String(dataBytes);
            }          
        }

    }
}
