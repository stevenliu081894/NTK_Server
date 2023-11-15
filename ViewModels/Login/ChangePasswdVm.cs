namespace NTKServer.ViewModels.Login
{
	public class ChangePasswdVm
	{
		/// <summary> 名称 </summary>
		public string LoginProvider { get; set; }

		/// <summary> 原始密码 </summary>
		public string ProviderKey { get; set; }

		/// <summary> 新密码 </summary>
		public string NewPassword { get; set; }

		/// <summary> 确认新密码 </summary>
		public string confirmPassword { get; set; }		
	}
}
