using NTKServer.Internal;
using System.Reflection;
using NTKServer.Libs;

namespace NTKServer.Tool
{
    /// <summary>
    /// 产生 where 字串
    /// </summary>
    public static class SqlTool
    {
        public static string Build<T>(T model)
        {
            string caseStr = string.Empty;
            string str = string.Empty;
            if (model == null) return str;

            Type type = model.GetType();
            PropertyInfo[] propertylist = type.GetProperties();

            foreach (PropertyInfo item in propertylist)
            {
                object? value = item.GetValue(model, null);
                if (item.PropertyType != typeof(bool))
                {
                    if (value == null || string.IsNullOrEmpty(value.ToString().Trim()))
                    {
                        continue;
                    }
                }

                if (item.IsDefined(typeof(WhereAttribute), false))
                {
                    WhereAttribute attribute = (WhereAttribute)item.GetCustomAttribute(typeof(WhereAttribute), false);
                    string field = attribute.field;
                    string comp = attribute.comparative;

                    if (str.Length == 0) str = " WHERE ";
                    else str += " AND ";

                    if (comp.ToLower() == "like")
                    {
                        str += string.Format(@"{0} LIKE '%{1}%'", field, value.ToString());
                    }
                    else
                    {
                        var val_string = value.ToString();
                        if (value.GetType() == typeof(DateTime)) {
                            val_string = Convert.ToDateTime(value).ToString("yyyy-MM-dd HH:mm:ss");
                            DateTime localTime = Convert.ToDateTime(value);
                            DateTime utcTime = localTime;
                            string time_zone_difference = ConfigLib.Get("time_zone_difference");
                            if (time_zone_difference != null) {
                                utcTime = localTime.AddHours(-Convert.ToInt32(time_zone_difference));
                            }
                           
                            val_string = utcTime.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        str += string.Format(@"{0} {1} '{2}'", field, comp, val_string);
                    }
                }
            }

            return str;
        }

        public static string Must(this string sourceStr, string sql)
        {
            string str;
            if (string.IsNullOrEmpty(sourceStr))
            {
                str = " WHERE " + sql;
            }
            else str = sourceStr.TrimEnd() + " AND " + sql;
            return str;
        }
    }
}
