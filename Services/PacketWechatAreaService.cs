using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class PacketWechatAreaService
    {
        #region Dto

        public static PacketWechatAreaDto Find(int pk)
        {
            string sql = @"SELECT * FROM `packet_wechat_area` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<PacketWechatAreaDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[PacketWechatAreaService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<PacketWechatAreaDto> FindAll()
        {
            string sql = @"SELECT * FROM `packet_wechat_area`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<PacketWechatAreaDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[PacketWechatAreaService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(PacketWechatAreaDto source)
        {
            string sql = @"INSERT INTO `packet_wechat_area` (
				`country`, `province`, `city`)
				VALUES (@country, @province, @city);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[PacketWechatAreaService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(PacketWechatAreaDto model)
        {
            string sql = @"UPDATE `packet_wechat_area` SET 
				`country` = @country,
				`province` = @province,
				`city` = @city
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[PacketWechatAreaService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `packet_wechat_area` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[PacketWechatAreaService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel

        #endregion

        #region Other

        #endregion
    }
}
