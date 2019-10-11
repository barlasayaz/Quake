using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quake.Toolkit.Helpers
{
    public class FormatHelper
    {
        public static CultureInfo CurrentUICulture
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentUICulture;
            }
        }

        public static string ToLocalizedString(decimal d)
        {
            var ci = CurrentUICulture;
            var groupSeparator = ci.NumberFormat.NumberGroupSeparator;
            var decimalSeparator = ci.NumberFormat.NumberDecimalSeparator;
            var groupCount = 3;
            

            var tempStr = d.ToString(CultureInfo.InvariantCulture);//tempStr: 1234.5
            var tempStrArray = tempStr.Split('.');//tempStrArray [0] 1234     [1] 5 
            var firstPart = tempStrArray[0];//firstPart 1234
            var secondPart = "";
            if (tempStrArray.Length > 1)
            {
                secondPart = tempStrArray[1];//secondPart  5
            }

            var rstr = "";
            for (int i = firstPart.Length; i > 0; i--)
            {
                rstr = firstPart[i - 1] + rstr;
                if (i != 1 && (rstr.Length % groupCount) == 0)
                {
                    rstr = groupSeparator + rstr;
                }
            }
            if (!string.IsNullOrEmpty(secondPart))
            {
                rstr += decimalSeparator + secondPart;
            }

            //var orgStr = d.ToString(CurrentUICulture);
            //if (System.Diagnostics.Debugger.IsAttached)
            //{
            //    System.Diagnostics.Debugger.Break();
            //}
            return rstr;//1,234.5
        }
    }
}
