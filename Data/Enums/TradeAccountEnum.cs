/// <summary>
/// 帳戶状态 0.可交易 1.冻结 2.清算中 3.已结束
/// </summary>
public enum AccountStatusType
{
	Tradable = 0,
	Freeze = 1,
	Settlement = 2,
	End = 3
}

public static class ConvertEnum
{
	public static string ConvertAccountStatus(int status)
	{
		return status switch
		{
			(int)AccountStatusType.Tradable => "可交易",
			(int)AccountStatusType.Freeze => "冻结",
			(int)AccountStatusType.Settlement => "清算中",
			(int)AccountStatusType.End => "已结束"
		};
	}
}