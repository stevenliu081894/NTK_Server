using Dapper;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Libs;
using NTKServer.ViewModels.TradeCancel;

namespace DB.Services
{
    public class TradeCancelService
    {
        #region Dto

        public static TradeCancelDto Find(int pk)
        {
            string sql = @"SELECT * FROM `trade_cancel` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.QueryFirstOrDefault<TradeCancelDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeCancelService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<TradeCancelDto> FindAll()
        {
            string sql = @"SELECT * FROM `trade_cancel`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<TradeCancelDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeCancelService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(TradeCancelDto source)
        {
            string sql = @"INSERT INTO `trade_cancel` (
				`trade_order_sn`, `sub_account`, `sn`, `market`, `stock_code`, `stock_name`, `request_volume`, `cancel_volume`, `order_ip`, `order_client`, `cancel_type`, `cancel_datetime`, `cancel_by`)
				VALUES (@trade_order_sn, @sub_account, @sn, @market, @stock_code, @stock_name, @request_volume, @cancel_volume, @order_ip, @order_client, @cancel_type, @cancel_datetime, @cancel_by);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeCancelService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(TradeCancelDto model)
        {
            string sql = @"UPDATE `trade_cancel` SET 
				`trade_order_sn` = @trade_order_sn,
				`sub_account` = @sub_account,
				`sn` = @sn,
				`market` = @market,
				`stock_code` = @stock_code,
				`stock_name` = @stock_name,
				`request_volume` = @request_volume,
				`cancel_volume` = @cancel_volume,
				`order_ip` = @order_ip,
				`order_client` = @order_client,
				`cancel_type` = @cancel_type,
				`cancel_datetime` = @cancel_datetime,
				`cancel_by` = @cancel_by
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeCancelService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `trade_cancel` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeCancelService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<TradeCancelList> FindTradeCancelList(string whereSql="")
        {
            string sql = @$"SELECT trade_cancel.cancel_datetime, trade_cancel.sub_account, trade_cancel.sn, trade_cancel.trade_order_sn, trade_cancel.stock_code, trade_cancel.stock_name, trade_cancel.cancel_volume, trade_cancel.cancel_type FROM `trade_cancel`{whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<TradeCancelList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeCancelService][FindTradeCancelList]" + ex.Message);
                return null;
            }
        }







        #endregion

        #region Other

        /// <summary>
        /// 增加撤單紀錄
        /// </summary>
        /// <param name="model"></param>
        public static int AddCancelOrderRecord(TradeOrderDto order, string adminIp, string adminName)
        {
            const string sql = @"INSERT INTO
    `usstock`.`trade_cancel` (
        `trade_order_sn`,
        `sub_account`,
        `market`,
        `stock_code`,
        `stock_name`,
        `request_volume`,
        `cancel_volume`,
        `order_ip`,
        `cancel_type`,
        `cancel_datetime`,
        `cancel_by`
    )
VALUES (
        @sn,
        @sub_account,
        @market,
        @stock_code,
        @stock_name,
        @request_volume,
        @cancel_volume,
        @order_ip,
        @cancel_type,
        @cancel_datetime,
        @cancel_by
    );
UPDATE trade_cancel SET sn =  CONCAT('C',DATE_FORMAT(NOW(), '%y'), LPAD(LAST_INSERT_ID(), 8, '0')) WHERE pk =LAST_INSERT_ID();
";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new
                {
                    sn = order.sn,
                    sub_account = order.sub_account,
                    market = order.market,
                    stock_code = order.stock_code,
                    stock_name = order.stock_name,
                    request_volume = order.free_volume,
                    cancel_volume = order.free_volume,
                    order_ip = adminIp,
                    cancel_type = 3,
                    cancel_datetime = DateTime.Now,
                    cancel_by = adminName,
                });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeCancelService][AddCancelOrderRecord]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        #endregion
    }
}
