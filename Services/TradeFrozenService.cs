using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using Microsoft.AspNetCore.Connections;
using StackExchange.Redis;

namespace DB.Services
{
    public class TradeFrozenService
    {
        #region Dto

        public static TradeFrozenDto Find(int pk)
        {
            string sql = @"SELECT * FROM `trade_frozen` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.QueryFirstOrDefault<TradeFrozenDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeFrozenService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<TradeFrozenDto> FindAll()
        {
            string sql = @"SELECT * FROM `trade_frozen`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<TradeFrozenDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeFrozenService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(TradeFrozenDto source)
        {
            string sql = @"INSERT INTO `trade_frozen` (
				`sub_account`, `trade_order_fk`, `info`, `type`, `frozen_volume`, `frozen_money`, `frozen_datetime`)
				VALUES (@sub_account, @trade_order_fk, @info, @type, @frozen_volume, @frozen_money, @frozen_datetime);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeFrozenService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(TradeFrozenDto model)
        {
            string sql = @"UPDATE `trade_frozen` SET 
				`sub_account` = @sub_account,
				`trade_order_fk` = @trade_order_fk,
				`info` = @info,
				`type` = @type,
				`frozen_volume` = @frozen_volume,
				`frozen_money` = @frozen_money,
				`frozen_datetime` = @frozen_datetime
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeFrozenService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `trade_frozen` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeFrozenService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel

        #endregion

        #region Other

        /// <summary>
        /// 撤销冻结的金额, 扣抵结果 = 0 直接整笔删除
        /// </summary>
        public static int CancelFrozenMoney(TradeOrderDto order)
        {
            const string sql = @"UPDATE trade_frozen SET frozen_money = CASE
    WHEN (frozen_money - @free_volume * @price) <= 0 THEN NULL
    ELSE frozen_money - @free_volume * @price
END
WHERE trade_order_fk=@trade_order_fk;

DELETE FROM trade_frozen
WHERE frozen_money IS NULL 
AND  trade_order_fk=@trade_order_fk;";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { trade_order_fk = order.pk, free_volume = order.free_volume, price = order.price });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeFrozenService][CancelFrozenMoney]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        /// <summary>
        /// 撤销冻结的量, 扣抵结果 = 0 直接整笔删除
        /// </summary>
        public static int CancelFrozenVolume(TradeOrderDto order)
        {
            const string sql = @"UPDATE trade_frozen
SET frozen_volume = CASE
    WHEN (frozen_volume - @free_volume) <= 0 THEN NULL
    ELSE frozen_volume - @free_volume
END
WHERE trade_order_fk = @trade_order_fk;

DELETE FROM trade_frozen
WHERE frozen_volume IS NULL 
AND  trade_order_fk = @trade_order_fk;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { trade_order_fk = order.pk, free_volume = order.free_volume });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeFrozenService][CancelFrozenVolume]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        /// <summary>
        /// 删除冻结记录
        /// </summary>
        public static int DelTradeFrozen(int tradeOrderFk)
        {
            string sql = $"DELETE FROM trade_frozen WHERE type = 1 AND trade_order_fk = @tradeOrderFk;";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, new
                {
                    tradeOrderFk,
                });
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeFrozenService][DelTradeFrozen]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        #endregion
    }
}

