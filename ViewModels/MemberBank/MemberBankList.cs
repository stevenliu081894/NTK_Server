using NTKServer.Internal;

namespace NTKServer.ViewModels.MemberBank
{
    public class MemberBankList
    {
        /// <summary> card_pk: 卡片pk(英文字母+隨機數字) </summary>
        public string card_pk { get; set; }

        /// <summary> card_type: 卡片类别 </summary>
        public int card_type { get; set; }

        /// <summary> currency: 币别 </summary>
        public string currency { get; set; }

        /// <summary> bank: 所属银行 </summary>
        public string bank { get; set; }

        /// <summary> branch: 分行 </summary>
        public string branch { get; set; }

        /// <summary> card: 銀行卡號 </summary>
        public string card { get; set; }

        /// <summary> bank_account: 持有人 </summary>
        public string bank_account { get; set; }

        /// <summary> is_confirm: 核实狀態 </summary>
        public bool is_confirm { get; set; }

        /// <summary> account: 会员帐号 </summary>
        public string account { get; set; }

        /// <summary> nickname: 名称 </summary>
        public string nickname { get; set; }

        /// <summary> real_name: 会员姓名 </summary>
        public string real_name { get; set; }
    }
}
