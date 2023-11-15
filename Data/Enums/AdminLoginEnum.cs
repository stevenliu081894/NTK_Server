using System.ComponentModel.DataAnnotations;

namespace NTKServer.Data.Enums
{
	/// <summary>
	/// 登入结果：1:成功 0:失敗
	/// </summary>
	public enum AdminLoginEnum
	{
		
		[Display(Name = "失敗")]
		Fail = 0,
		[Display(Name = "成功")]
		Success = 1,
	}
}
