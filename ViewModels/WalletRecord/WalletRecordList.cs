using NTKServer.Internal;

namespace NTKServer.ViewModels.WalletRecord
{
    public class WalletRecordList
    {
        /// <summary> admin_user_fk: 会员编号 </summary>
        public int admin_user_fk { get; set; }
        /// <summary> pk: # </summary>
        public int pk { get; set; }

        /// <summary> type: 资金类型 </summary>
        public int type { get; set; }

        /// <summary> currency: 币别 </summary>
        public string currency { get; set; }

        /// <summary> affect: 影响金额 </summary>
        public decimal affect { get; set; }

        /// <summary> freeze: 冻结金额 </summary>
        public decimal freeze { get; set; }

        /// <summary> balance: 账户可用余额 </summary>
        public decimal balance { get; set; }

        /// <summary> coupon: 折扣金余额 </summary>
        public decimal coupon { get; set; }

        /// <summary> info: 详情 </summary>
        public string info { get; set; }

        /// <summary> create_time: 交易时间 </summary>
        public DateTime create_time { get; set; }

        /// <summary> create_ip: 交易IP </summary>
        public string create_ip { get; set; }

    }
}
