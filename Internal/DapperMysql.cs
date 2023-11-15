using Dapper;
using MySql.Data.MySqlClient;

namespace NTKServer.Internal
{
    public class DapperMysql
    {
        private static string? _writeConn;
        private static string? _readConn;

        public static void Init(string? writeConn, string? readConn)
        {
            _writeConn = writeConn;
            _readConn = readConn;
        }

        public static MySqlConnection? GetReadConnection()
        {
            var conn = new MySqlConnection(_readConn);
            conn.Open();
            return conn;
        }

        public static MySqlConnection? GetWriteConntion()
        {
            var conn = new MySqlConnection(_writeConn);
            conn.Open();
            return conn;
        }

        /// <summary>
        /// 把物件所有屬性當成參數回傳
        /// </summary>
        public static DynamicParameters GetParameters(object obj)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            var props = obj.GetType().GetProperties();
            foreach (var prop in props)
            {
                dynamicParameters.Add("@" + prop.Name, prop.GetValue(obj));
            }
            dynamicParameters.RemoveUnused = true;
            return dynamicParameters;
        }
    }
}
