using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Quake.Toolkit.Helpers
{
    public static class ExtensionMethods
    {
        public static T ConvertTo<T>(this object obj)
        {
            T result = default(T);
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter != null && converter.CanConvertFrom(obj.GetType()))
            {
                result = (T)converter.ConvertFrom(null, System.Globalization.CultureInfo.InvariantCulture, obj);
            }
            else
            {
                converter = TypeDescriptor.GetConverter(obj);
                if (converter != null && converter.CanConvertTo(obj.GetType()))
                {
                    result = (T)converter.ConvertTo(null, System.Globalization.CultureInfo.InvariantCulture, obj, obj.GetType());
                }
            }
            return result;
        }

        public static string ToMd5Hash(this string str)
        {
            MD5 md5 = MD5.Create();
            byte[] btr = Encoding.UTF8.GetBytes(str);
            btr = md5.ComputeHash(btr);
            var sb = new StringBuilder();
            foreach (byte ba in btr)
            {
                sb.Append(ba.ToString("x2").ToLower());
            }
            return sb.ToString();
        }

        public static List<T> Clone<T>(this List<T> oldList)
        {
            return new List<T>(oldList);
        }

        public static string GetAllNamespaceAndMethodName(this System.Reflection.MethodBase methodBase)
        {
            var rval = string.Concat(methodBase.DeclaringType.FullName, ".", methodBase.Name);
            return rval;
        }

        public static string ConcatStr(this string s, string concatStr)
        {
            return string.Concat(s, concatStr);
        }

        public static string GetDescription<T>(this T enumerationValue) where T : struct
        {
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            }

            //Tries to find a DescriptionAttribute for a potential friendly name
            //for the enum
            var memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    //Pull out the description value
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            //If we have no description attribute, just return the ToString of the enum
            return enumerationValue.ToString();
        }
    }
}
