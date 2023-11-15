using System.ComponentModel.DataAnnotations;

namespace NTKServer.Data.Enums
{
	public enum CmsPopInfoEnum
	{
		/// <summary>
		/// 尺寸 1.手机端 2.pc
		/// </summary>
		[Display(Name = "手机端")]
		Mobile = 1,
		[Display(Name = "PC")]
		PC = 2,
	}

	public static class CmsPopInfoConvertEnum
	{
		public static string ConvertSize(int size)
		{
			return size switch
			{
				(int)CmsPopInfoEnum.Mobile => "手机端",
				(int)CmsPopInfoEnum.PC => "PC",
			};
		}
	}
}
