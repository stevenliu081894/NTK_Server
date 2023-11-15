namespace NTKServer.ViewModels.Demo
{
    public class DemoMemberVm
    {
        /// <summary> id:会员编号</summary>
        public int id { get; set; }

        /// <summary> account:会员帐号</summary>
        public string account { get; set; }

        /// <summary> realName:会员姓名</summary>       
        public string realName { get; set; }

        /// <summary> email:信箱</summary>               
        public string email { get; set; }

        /// <summary> birthdat:生日</summary>
        public DateTime birthday { get; set; }

        /// <summary> isSucceed:儲存結果</summary>
        public bool isSucceed { get; set; }
    }
}
