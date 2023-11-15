using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class MemberDto
    {
        /// <summary> 所属服务客服(营业员) </summary>
        public int? admin_user_fk { get; set; }
        /// <summary> 用户pk </summary>
        public int pk { get; set; }
        /// <summary> 運營商的會員編號 </summary>
        public int id { get; set; }
        /// <summary> 登入帐号 </summary>
        public string account { get; set; }
        /// <summary> 名称 </summary>
        public string nickname { get; set; }
        /// <summary> 真实姓名 </summary>
        public string real_name { get; set; }
        /// <summary> 电子信箱用于登陆 </summary>
        public string email { get; set; }
        /// <summary> 手機國家碼 </summary>
        public string mobile_country { get; set; }
        /// <summary> 手机号 </summary>
        public string mobile { get; set; }
        /// <summary> 登陆密码 </summary>
        public string passwd { get; set; }
        /// <summary> 交易端 token </summary>
        public string token { get; set; }
        /// <summary> 目前使用中交易号 </summary>
        public string sub_account { get; set; }
        /// <summary> 支付密码 </summary>
        public string paywd { get; set; }
        /// <summary> 證件號号码 </summary>
        public string id_card { get; set; }
        /// <summary> 0: 其他, 1: 身份证, 2: 护照 </summary>
        public int id_card_type { get; set; }
        /// <summary> 身份证是否通过校验 0未做过  1通过 2错误 3.审核中 </summary>
        public int id_auth { get; set; }
        /// <summary> 0： 禁止登录  1 ：正常使用   </summary>
        public bool status { get; set; }
        /// <summary> 是否删除 1 已删除或注销 </summary>
        public bool is_del { get; set; }
        /// <summary> 注册时间 </summary>
        public DateTime create_time { get; set; }
        /// <summary> 注册IP </summary>
        public string create_ip { get; set; }
        /// <summary> 最后一次登录时间 </summary>
        public DateTime last_login_time { get; set; }
        /// <summary> 最后一次登录ip </summary>
        public string last_login_ip { get; set; }
        /// <summary> 紧急联系人姓名 </summary>
        public string urgent_name { get; set; }
        /// <summary> 紧急联系人手机号 </summary>
        public int urgent_mobile { get; set; }
        /// <summary> 实名认证申请时间 </summary>
        public DateTime auth_time { get; set; }
        /// <summary> 实名认证结果说明 </summary>
        public string auth_result { get; set; }
        /// <summary> 用户头像(link) </summary>
        public string head_img { get; set; }
        /// <summary> 证件照正面 </summary>
        public string card_pic_front { get; set; }
        /// <summary> 证件照背面 </summary>
        public string card_pic_back { get; set; }
        /// <summary> 出金银行存折照片 </summary>
        public string card_pic_hand { get; set; }
        /// <summary> 專屬邀请码 </summary>
        public string invitation_code { get; set; }
        /// <summary> 注册输入的推荐码 </summary>
        public string recommend { get; set; }
        /// <summary> 推荐人id </summary>
        public int recommend_id { get; set; }
        /// <summary> 身份证图片 </summary>
        public int card_pic { get; set; }
        /// <summary> 会员等级 </summary>
        public string level_id { get; set; }
        /// <summary> 备注内容 </summary>
        public string remark { get; set; }
        /// <summary> 国家 sys_country pk </summary>
        public string country { get; set; }
        /// <summary> 时区 TimeZone Id </summary>
        public string time_zone { get; set; }
        /// <summary> 语系 </summary>
        public string lang { get; set; }
        /// <summary> 0: 未验证, 1: 验证成功, ,2: 验证失败 </summary>
        public bool sms_status { get; set; }
        /// <summary> 0: 未验证, 1: 验证成功, ,2: 验证失败 </summary>
        public bool email_status { get; set; }
    }
}
