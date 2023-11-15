using NTKServer.Internal;

namespace NTKServer.ViewModels.BorrowFee
{
    public class BorrowFeeList
    {
        /// <summary> create_time: 收費时间 </summary>
        public DateTime create_time { get; set; }

        /// <summary> type: 类型 </summary>
        public int type { get; set; }

        /// <summary> borrow_fee: 管理费 </summary>
        public decimal borrow_fee { get; set; }

        /// <summary> sub_account: 交易帐号 </summary>
        public string sub_account { get; set; }

        /// <summary> loan_type: 帳號類型 </summary>
        public string loan_type { get; set; }

        /// <summary> currency: 市場币种 </summary>
        public string currency { get; set; }

        /// <summary> borrow_duration: 合约时长 </summary>
        public int borrow_duration { get; set; }

        /// <summary> account: 会员帐号 </summary>
        public string account { get; set; }

        /// <summary> member_name: 会员姓名 </summary>
        public string member_name { get; set; }

    }
}
