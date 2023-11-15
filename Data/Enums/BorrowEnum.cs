using System.ComponentModel.DataAnnotations;

/// <summary>
/// 配资状态 -1：待审核  0：未通过 1：使用中  2：已结束 3：已逾期
/// </summary>
public enum BorrowStatus
{
	[Display(Name = "待审核")]
	Default = -1,
	[Display(Name = "未通过")]
	Forbid = 0,
	[Display(Name = "使用中")]
	Using = 1,
	[Display(Name = "已结束")]
	End = 2,
	[Display(Name = "已逾期")]
	Expired = 3,
}


public static class BorrowConvertEnum
{
	public static string ConvertBorrowStatus(int status)
	{
		return status switch
		{
			(int)BorrowStatus.Default => "待审核",
			(int)BorrowStatus.Forbid => "未通过",
			(int)BorrowStatus.Using => "使用中",
			(int)BorrowStatus.End => "已结束",
			(int)BorrowStatus.Expired => "已逾期"
		};
	}
}