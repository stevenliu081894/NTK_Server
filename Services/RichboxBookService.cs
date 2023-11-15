using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class RichboxBookService
    {
        #region Dto

        public static RichboxBookDto Find(int member_fk)
        {
            string sql = @"SELECT * FROM `richbox_book` WHERE `member_fk` = @member_fk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {member_fk});
                return conn.QueryFirstOrDefault<RichboxBookDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RichboxBookService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<RichboxBookDto> FindAll()
        {
            string sql = @"SELECT * FROM `richbox_book`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<RichboxBookDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[RichboxBookService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int Insert(RichboxBookDto model)
        {
			string sql = @"INSERT INTO `richbox_book` (
				`member_fk`, `total_assets`, `profit`, `day_earning`, `total_earing`, `max_assets`)
				VALUES (@member_fk, @total_assets, @profit, @day_earning, @total_earing, @max_assets); ";

			try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RichboxBookService][Insert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(RichboxBookDto model)
        {
            string sql = @"UPDATE `richbox_book` SET 
				`total_assets` = @total_assets,
				`profit` = @profit,
				`day_earning` = @day_earning,
				`total_earing` = @total_earing,
				`max_assets` = @max_assets
				 WHERE `member_fk` = @member_fk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RichboxBookService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int member_fk)
        {
            string sql = @"DELETE FROM `richbox_book` WHERE `member_fk` = @member_fk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { member_fk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RichboxBookService][Remove]" + ex.Message);
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
