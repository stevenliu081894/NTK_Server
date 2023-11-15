using NTKServer.Internal;

namespace NTKServer.ViewModels.AdminBank
{
    public class AdminBankList
    {
        /// <summary> pk: # </summary>
        public int pk { get; set; }

        /// <summary> type: 钱包类别 </summary>
        public int type { get; set; }

        /// <summary> currency: 收款币别 </summary>
        public string currency { get; set; }

        /// <summary> card: 银行卡号 </summary>
        public string card { get; set; }

        /// <summary> bank_name: 所属银行 </summary>
        public string bank_name { get; set; }

        /// <summary> open_bank: 分行 </summary>
        public string open_bank { get; set; }

        /// <summary> payee: 收款人 </summary>
        public string payee { get; set; }

        /// <summary> notes: 说明 </summary>
        public string notes { get; set; }

        /// <summary> status: 启用 </summary>
        public int status { get; set; }

        /// <summary> viplists: 可见等级 </summary>
        public string viplists { get; set; }

    }
}
