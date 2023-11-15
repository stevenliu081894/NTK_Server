using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class StockFavoriteService
    {
        #region Dto

        public static StockFavoriteDto Find(int pk)
        {
            string sql = @"SELECT * FROM `stock_favorite` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<StockFavoriteDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[StockFavoriteService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<StockFavoriteDto> FindAll()
        {
            string sql = @"SELECT * FROM `stock_favorite`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<StockFavoriteDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[StockFavoriteService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(StockFavoriteDto source)
        {
            string sql = @"INSERT INTO `stock_favorite` (
				`member_fk`, `stock_code`, `market`)
				VALUES (@member_fk, @stock_code, @market);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[StockFavoriteService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(StockFavoriteDto model)
        {
            string sql = @"UPDATE `stock_favorite` SET 
				`member_fk` = @member_fk,
				`stock_code` = @stock_code,
				`market` = @market
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[StockFavoriteService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `stock_favorite` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[StockFavoriteService][Remove]" + ex.Message);
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
