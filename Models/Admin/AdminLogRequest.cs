using System;
namespace NTKServer.Models.Admin
{
	public class AdminLogRequest
	{
        public int admin_user_pk { get; set; }
        public int admin_menu_fk { get; set; }
        public DateTime createtime { get; set; }
        public object[]? list { get; set; }
    }
}

