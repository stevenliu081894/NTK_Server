using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.TradeAccount;
using X.PagedList;
using Microsoft.AspNetCore.Connections;
using NTKServer.ViewModels.UserTradeOrder;

namespace DB.Services
{
    public class TradeOrderService
    {
        #region Dto

        public static TradeOrderDto Find(int pk)
        {
            string sql = @"SELECT * FROM `trade_order` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.QueryFirstOrDefault<TradeOrderDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeOrderService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<TradeOrderDto> FindAll()
        {
            string sql = @"SELECT * FROM `trade_order`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<TradeOrderDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeOrderService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(TradeOrderDto source)
        {
            string sql = @"INSERT INTO `trade_order` (
				`sub_account`, `sn`, `stock_code`, `stock_name`, `market`, `dir`, `order_type`, `price_type`, `price`, `status`, `volume`, `free_volume`, `succeed_volume`, `cancel_volume`, `order_time`, `order_ip`, `order_client`, `order_source`, `cancel_datetime`, `cancel_type`, `cancel_by`, `live_mode`, `live_ordersn`, `live_request`, `live_succeed`, `live_price`)
				VALUES (@sub_account, @sn, @stock_code, @stock_name, @market, @dir, @order_type, @price_type, @price, @status, @volume, @free_volume, @succeed_volume, @cancel_volume, @order_time, @order_ip, @order_client, @order_source, @cancel_datetime, @cancel_type, @cancel_by, @live_mode, @live_ordersn, @live_request, @live_succeed, @live_price);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeOrderService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(TradeOrderDto model)
        {
            string sql = @"UPDATE `trade_order` SET 
				`sub_account` = @sub_account,
				`sn` = @sn,
				`stock_code` = @stock_code,
				`stock_name` = @stock_name,
				`market` = @market,
				`dir` = @dir,
				`order_type` = @order_type,
				`price_type` = @price_type,
				`price` = @price,
				`status` = @status,
				`volume` = @volume,
				`free_volume` = @free_volume,
				`succeed_volume` = @succeed_volume,
				`cancel_volume` = @cancel_volume,
				`order_time` = @order_time,
				`order_ip` = @order_ip,
				`order_client` = @order_client,
				`order_source` = @order_source,
				`cancel_datetime` = @cancel_datetime,
				`cancel_type` = @cancel_type,
				`cancel_by` = @cancel_by,
				`live_mode` = @live_mode,
				`live_ordersn` = @live_ordersn,
				`live_request` = @live_request,
				`live_succeed` = @live_succeed,
				`live_price` = @live_price
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeOrderService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `trade_order` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeOrderService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<UserTradeOrderList> FindUserTradeOrderList(string whereSql="")
        {
            string sql = @$"SELECT trade_order.pk, trade_order.sub_account, trade_order.order_time, trade_order.sn, trade_order.stock_code, 
                trade_order.stock_name, trade_order.market, trade_order.dir, trade_order.order_type, trade_order.price_type, trade_order.price, 
                trade_order.status, trade_order.volume, trade_order.free_volume, trade_order.succeed_volume, trade_order.cancel_volume 
                FROM `trade_order`{whereSql} order by order_time ";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<UserTradeOrderList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeOrderService][FindUserTradeOrderList]" + ex.Message);
                return null;
            }
        }

        public static List<TradeStopSearchList> FindTradeOrderSearch(string where = "")
        {
            string sql = $"SELECT * FROM `trade_order` {where} ORDER BY `cancel_datetime` DESC";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<TradeStopSearchList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeOrderService][FindTradeOrderSearch]" + ex.Message);
                return null;
            }
        }

        public static List<TradeEntrustedSearchList> FindEntrustedOrderSearch(string where = "")
        {
            string sql = $"SELECT * FROM `trade_order` {where} ORDER BY `order_time` DESC";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<TradeEntrustedSearchList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeOrderService][TradeEntrustedSearchList]" + ex.Message);
                return null;
            }
        }

        #endregion

        #region Other

        /// <summary>
        /// 更新撤單未成量
        /// </summary>
        public static int UpdateCancelOrderVolume(int pk)
        {
            const string sql = "UPDATE `trade_order` SET cancel_volume=free_volume, free_volume=0, status = 2  WHERE pk=@pk;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeOrderService][UpdateCancelOrderVolume]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        #endregion
    }
}
