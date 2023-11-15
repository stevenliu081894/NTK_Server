using NTKServer.Internal;

namespace NTKServer.ViewModels.RecommendReward
{
    public class RecommendRewardFilter
    {
        /// <summary> account: 会员帐号 </summary>
        [Where("=", "member.account")]
        public string? account { get; set; }

        /// <summary> nickname: 名称 </summary>
        [Where("LIKE", "member.nickname")]
        public string? nickname { get; set; }

        /// <summary> year: 年 </summary>
        public string? year { get; set; }

        /// <summary> month: 月 </summary>
        public string? month { get; set; }

    }
}
