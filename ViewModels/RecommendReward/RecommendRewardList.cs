using NTKServer.Data.Enums;

namespace NTKServer.ViewModels.RecommendReward
{
    public class RecommendRewardList
    {
        public int pk {  get; set; }
        /// <summary> account: 会员帐号 </summary>
        public string account { get; set; }

        /// <summary> nickname: 名称 </summary>
        public string nickname { get; set; }

        /// <summary> year: 年 </summary>
        public string year { get; set; }

        /// <summary> month: 月 </summary>
        public string month { get; set; }

        /// <summary> currency: 币别 </summary>
        public string currency { get; set; }

        /// <summary> total_reward: 分潤總額 </summary>
        public decimal total_reward { get; set; }

        /// <summary> state: 状态 </summary>
        public int state { get; set; }
		/// <summary> state: 状态 </summary>
		public string stateText 
        {
			get
			{
				return RecommendRewardConvertEnum.ConvertStatus(state);
			}
		}

		/// <summary> withdraw: 申请提现时间 </summary>
		public DateTime? withdraw { get; set; }

        /// <summary> paydate: 发放佣金时间 </summary>
        public DateTime? paydate { get; set; }

        /// <summary> create_time: 創建時間 </summary>
        public DateTime create_time { get; set; }

    }
}
