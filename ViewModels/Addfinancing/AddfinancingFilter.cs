using NTKServer.Internal;

namespace NTKServer.ViewModels.Addfinancing
{
    public class AddfinancingFilter
    {
        /// <summary> pk: 编号 </summary>
        [Where("=", "pk")]
        public int? pk { get; set; }

        /// <summary> sub_account: 交易帐号 </summary>
        [Where("=", "sub_account")]
        public string? sub_account { get; set; }

        /// <summary> member_fk: 会员编号 </summary>
        [Where("=", "member_fk")]
        public int? member_fk { get; set; }

        /// <summary> account: 会帐号 </summary>
        [Where("=", "account")]
        public string? account { get; set; }

        /// <summary> member_name: 姓名 </summary>
        [Where("LIKE", "member_name")]
        public string? member_name { get; set; }

    }
}
