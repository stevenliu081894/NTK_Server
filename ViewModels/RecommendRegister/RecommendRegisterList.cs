namespace NTKServer.ViewModels.RecommendRegister
{
    public class RecommendRegisterList
    {
        public int invitee_fk { get; set; }
        /// <summary> create_date: 注册时间 </summary>
        public DateTime create_date { get; set; }

        /// <summary> admin_user_fk: 所属客服 </summary>
        public int admin_user_fk { get; set; }

        /// <summary> account: 会员帐号 </summary>
        public string account { get; set; }

        /// <summary> nickname: 会员名称 </summary>
        public string nickname { get; set; }

        /// <summary> real_name: 真实姓名 </summary>
        public string real_name { get; set; }

        /// <summary> id_auth: 身份校验 </summary>
        public int id_auth { get; set; }

        /// <summary> level_id: 会员等级 </summary>
        public string level_id { get; set; }

        /// <summary> invitation_code: 邀请码 </summary>
        public string invitation_code { get; set; }

        /// <summary> first_borrow_date: 第一筆交易 </summary>
        public DateTime? first_borrow_date { get; set; }

        /// <summary> Inviteaccount: 邀请人帐号 </summary>
        public string Inviteaccount { get; set; }

        /// <summary> Invitenickname: 邀请人名称 </summary>
        public string Invitenickname { get; set; }

    }
}
