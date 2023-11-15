namespace NTKServer.ViewModels.StatWalletRecordDetail
{
    public class StatWalletRecordDetailList
    {
        /// <summary> admin_user_fk: 所属客服 </summary>
        public int admin_user_fk { get; set; }
        public string admin_account { get; set; }

        /// <summary> account: 会员帐号 </summary>
        public string account { get; set; }

        /// <summary> nickname: 名称 </summary>
        public string nickname { get; set; }

        /// <summary> type: 资金类型 </summary>
        public int type { get; set; }
        
        /// <summary> type: 资金类型 </summary>
        public string template_name { get; set; }

        /// <summary> affect: 影响金额 </summary>
        public decimal affect { get; set; }

        /// <summary> create_time: 交易时间 </summary>
        public DateTime create_time { get; set; }

    }
}
