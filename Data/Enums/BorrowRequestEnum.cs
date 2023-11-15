/// <summary>
/// 訂單審核状态 0.审核中 1.成功，2.失败
/// </summary>
public enum VerifyStatusType
{
	Processing = 0,
	Successful = 1,
	Fail = 2
}

/// <summary>
/// 业务类型 1.新合约 2.续期
/// </summary>
public enum BorrowTypeType
{
	NewContract = 1,
	Renew = 2
}

public static class BorrowRequestConvertEnum
{
	public static string ConvertVerifyStatus(int status)
	{
		return status switch
		{
			(int)VerifyStatusType.Processing => "审核中",
			(int)VerifyStatusType.Successful => "成功",
			(int)VerifyStatusType.Fail => "失败",
		};
	}

	public static string ConvertBorrowType(int status)
	{
		return status switch
		{
			(int)BorrowTypeType.NewContract => "新合约",
			(int)BorrowTypeType.Renew => "续期"
		};
	}
}

