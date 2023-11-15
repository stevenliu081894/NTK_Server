using MySqlX.XDevAPI.Relational;
using NTKServer.Internal;

namespace NTKServer.ViewModels.ReviewMember
{
    public class ReviewMemberList
    {
        /// <summary> pk: 用户pk </summary>
        public int pk { get; set; }

        /// <summary> account: 登入帐号 </summary>
        public string account { get; set; }

        /// <summary> nickname: 名称 </summary>
        public string nickname { get; set; }

        /// <summary> real_name: 真实姓名 </summary>
        public string real_name { get; set; }

        /// <summary> email: 电子信箱用于登陆 </summary>
        public string email { get; set; }

        /// <summary> create_time: 注册时间 </summary>
        public DateTime create_time { get; set; }

        /// <summary> create_ip: 注册IP </summary>
        public string create_ip { get; set; }

        /// <summary> last_login_time: 最后一次登录时间 </summary>
        public DateTime last_login_time { get; set; }

        /// <summary> last_login_ip: 最后一次登录ip </summary>
        public string last_login_ip { get; set; }

        /// <summary> auth_time: 实名认证申请时间 </summary>
        public DateTime auth_time { get; set; }

        /// <summary> auth_result: 实名认证结果说明 </summary>
        public string auth_result { get; set; }

        /// <summary> recommend_id: 推荐人id </summary>
        public int recommend_id { get; set; }

        /// <summary> country: 国家 </summary>
        public string country { get; set; }

        /// <summary> lang: 语系 </summary>
        public string lang { get; set; }
        
        /// <summary> lang: 语系 </summary>
        public int id_auth { get; set; }

    }

    public class VerifyMemberVm
    {
		/// <summary> 用户pk </summary>
		public int pk { get; set; }
		/// <summary> 真实姓名 </summary>
		public string real_name { get; set; }
		/// <summary> 手機國家碼 </summary>
		public string mobile_country { get; set; }
		/// <summary> 手机号 </summary>
		public string mobile { get; set; }
		/// <summary> 0: 其他, 1: 身份证, 2: 护照 </summary>
		public int id_card_type { get; set; }
		/// <summary> 电子信箱用于登陆 </summary>
		public string email { get; set; }
		/// <summary> 验证 </summary>
		public string idCardTxt
		{
			get
			{
				return id_card_type switch
				{
					0 => "其他",
					1 => "身份证",
					2 => "护照",
				};
			}
		}
		/// <summary> 身份证是否通过校验 0未做过  1通过 2错误 3.审核中 </summary>
		public bool id_auth { get; set; }
		/// <summary> 证件照正面 </summary>
		public string card_pic_front { get; set; } = "";
		/// <summary> 证件照背面 </summary>
		public string card_pic_back { get; set; }
		/// <summary> 0: 未验证, 1: 验证成功, ,2: 验证失败 </summary>
		public bool sms_status { get; set; }
		/// <summary> 验证 </summary>
		public string smsStatusTxt
		{
			get
			{
				return sms_status switch
				{
					true => "验证成功",
					false => "验证失败"
				};
			}
		}
		/// <summary> 0: 未验证, 1: 验证成功, ,2: 验证失败 </summary>
		public bool email_status { get; set; }
		/// <summary> 验证 </summary>
		public string emailStatusTxt
        {
			get
			{
				return email_status switch
				{
					true => "验证成功",
					false => "验证失败"
				};
			}
		}
		/// <summary> 所属服务客服(营业员) </summary>
		public int? admin_user_fk { get; set; }
		/// <summary> 会员等级 </summary>
		public int level_id { get; set; } = 0;
		/// <summary> 实名认证结果说明 </summary>
		public string auth_result { get; set; }
		/// <summary> 審核結果 </summary>
		public int verify_status { get; set; }
	}
}
