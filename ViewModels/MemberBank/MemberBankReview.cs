namespace NTKServer.ViewModels.MemberBank
{
    public class MemberBankReview
    {
        /// <summary> member_fk: 会员FK </summary>
        public int member_fk { get; set; }

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

        /// <summary> create_ip: 建立IP </summary>
        public string create_ip { get; set; }

        /// <summary> create_time: 建立时间 </summary>
        public DateTime create_time { get; set; }

        /// <summary> account: 会员帐号 </summary>
        public string account { get; set; }

        /// <summary> nickname: 名称 </summary>
        public string nickname { get; set; }

        /// <summary> real_name: 会员姓名 </summary>
        public string real_name { get; set; }

        /// <summary> id_auth: 身分驗證 </summary>
        public int id_auth { get; set; }

        /// <summary> card_front: 銀行卡正面照片 </summary>
        public string card_front { get; set; }

    }
}
