using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.TradeMoneyRecord;

namespace DB.Services
{
    public class TradeMoneyRecordService
    {
        #region Dto

        public static TradeMoneyRecordDto Find(int pk)
        {
            string sql = @"SELECT * FROM `trade_money_record` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.QueryFirstOrDefault<TradeMoneyRecordDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeMoneyRecordService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<TradeMoneyRecordDto> FindAll()
        {
            string sql = @"SELECT * FROM `trade_money_record`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<TradeMoneyRecordDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeMoneyRecordService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(TradeMoneyRecordDto source)
        {
            string sql = @"INSERT INTO `trade_money_record` (
				`member_fk`, `sub_account`, `trade_deal_fk`, `sn`, `temp_id`, `op`, `currency`, `balance`, `affect`, `exchange`, `wallet_amount`, `info`, `reviewer`, `create_datetime`, `market`, `order_type`, `stock_code`, `stock_name`)
				VALUES (@member_fk, @sub_account, @trade_deal_fk, @sn, @temp_id, @op, @currency, @balance, @affect, @exchange, @wallet_amount, @info, @reviewer, @create_datetime, @market, @order_type, @stock_code, @stock_name);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeMoneyRecordService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(TradeMoneyRecordDto model)
        {
            string sql = @"UPDATE `trade_money_record` SET 
				`member_fk` = @member_fk,
				`sub_account` = @sub_account,
				`trade_deal_fk` = @trade_deal_fk,
				`sn` = @sn,
				`temp_id` = @temp_id,
				`op` = @op,
				`currency` = @currency,
				`balance` = @balance,
				`affect` = @affect,
				`exchange` = @exchange,
				`wallet_amount` = @wallet_amount,
				`info` = @info,
				`reviewer` = @reviewer,
				`create_datetime` = @create_datetime,
				`market` = @market,
				`order_type` = @order_type,
				`stock_code` = @stock_code,
				`stock_name` = @stock_name
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeMoneyRecordService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `trade_money_record` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeMoneyRecordService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Insert(TradeMoneyRecordDto model)
        {
            const string sql = "INSERT INTO trade_money_record (sub_account, member_fk, trade_deal_fk, sn, temp_id, op,currency, balance, affect, exchange,wallet_amount,info,reviewer,create_datetime,market,stock_code,stock_name) " +
                               "VALUES(@sub_account, @member_fk, @trade_deal_fk, @sn, @temp_id, @op, @currency, @balance, @affect, @exchange, @wallet_amount, @info, @reviewer, @create_datetime, @market, @stock_code, @stock_name);";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, new
                {
                    sub_account = model.sub_account,
                    member_fk = model.member_fk,
                    trade_deal_fk = model.trade_deal_fk,
                    sn = model.sn,
                    temp_id = model.temp_id,
                    op = model.op,
                    currency = model.currency,
                    balance = model.balance,
                    affect = model.affect,
                    exchange = model.exchange,
                    wallet_amount = model.wallet_amount,
                    info = model.info,
                    reviewer = model.reviewer,
                    create_datetime = model.create_datetime,
                    market = model.market,
                    stock_code = model.stock_code,
                    stock_name = model.stock_name,
                });
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeMoneyRecordService][Insert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }

        }

        #endregion

        #region ViewModel
	    public static List<TradeMoneyRecordList> FindTradeMoneyRecordList(string whereSql="")
        {
            string sql = @$"SELECT trade_template.name AS temp_name , trade_money_record.create_datetime, trade_money_record.sub_account, trade_money_record.sn, trade_money_record.temp_id, trade_money_record.info, trade_money_record.affect, trade_money_record.exchange, trade_money_record.wallet_amount, trade_money_record.balance FROM `trade_money_record` 
                        LEFT JOIN `trade_template` ON trade_template.temp_id=trade_money_record.temp_id
                        {whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<TradeMoneyRecordList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeMoneyRecordService][FindTradeMoneyRecordList]" + ex.Message);
                return null;
            }
        }







        #endregion

        #region Other

        /// <summary>
        /// 依據PK，查詢數據
        /// </summary>
        /// <returns></returns>
        public static TradeMoneyRecordDto GetByAccount(string account)
        {
            string sql = $"SELECT * FROM `trade_money_record` WHERE sub_account = {account} ORDER BY create_datetime DESC ";
            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.QueryFirstOrDefault<TradeMoneyRecordDto>(sql);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeMoneyRecordService][GetByAccount]" + ex.Message);
                return null;
            }
        }

        #endregion
    }
}
