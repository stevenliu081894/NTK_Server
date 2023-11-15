using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NTKServer.Data.Enums
{
	public enum RecommendRewardEnum
	{
		/// <summary>
		/// 状态: -1_计算中 0_未提现 1_申请提现中 2_已提现  3_异常
		/// </summary>
		[Display(Name = "计算中")]
		Calculating = -1,
		[Display(Name = "未提现")]
		UnWithdraw = 0,
		[Display(Name = "申请提现中")]
		Processing = 1,
		[Display(Name = "已提现")]
		Done = 2,
		[Display(Name = "异常")]
		Fatal = 3
	}

	public static class RecommendRewardConvertEnum
	{
		public static string ConvertStatus(int size)
		{
			return size switch
			{
				(int)RecommendRewardEnum.Calculating => "计算中",
				(int)RecommendRewardEnum.UnWithdraw => "未提现",
				(int)RecommendRewardEnum.Processing => "申请提现中",
				(int)RecommendRewardEnum.Done => "已提现",
				(int)RecommendRewardEnum.Fatal => "异常",
			};
		}
	}
}
