using NTKServer.Internal;

namespace NTKServer.ViewModels.RewardDetail
{
    public class RewardDetailFilter
    {
        /// <summary> borrow_date: 交易日期 </summary>
        [Where("=", "recommend_reward_detail.borrow_date")]
        public DateTime? borrow_date { get; set; }

        /// <summary> account: 下线会员帐号 </summary>
        [Where("=", "member.account")]
        public string? account { get; set; }

        /// <summary> nickname: 下线会员名称 </summary>
        [Where("=", "member.nickname")]
        public string? nickname { get; set; }

    }
}
