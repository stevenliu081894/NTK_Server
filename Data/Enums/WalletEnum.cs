using System.ComponentModel.DataAnnotations;
/// <summary>
/// 充值審核狀態，0待处理 1 成功 2 失败
/// </summary>
public enum VerifyStatus
{
	[Display(Name = "待处理")]
	Processing = 0,
	[Display(Name = "成功")]
	Success = 1,
	[Display(Name = "失败")]
	Fail = 2
}

/// <summary>
/// 充值審核狀態，0待处理 1 成功 2 失败
/// </summary>
public enum WalletRechargeVerifyStatus
{
	[Display(Name = "待处理")]
	Processing = 0,
	[Display(Name = "成功")]
	Success = 1,
	[Display(Name = "失败")]
	Fail = 2
}

/// <summary>
/// 提现状态0处理中 1 提现成功 2 提现失败 3提现退回
/// </summary>
public enum WalletWithdrawStatus
{
	[Display(Name = "待处理")]
	Processing = 0,
	[Display(Name = "成功")]
	Success = 1,
	[Display(Name = "失败")]
	Fail = 2,
    [Display(Name = "3提现退回")]
    Reject = 3
}

public static class WalletRechargeConvertEnum
{
	public static string ConvertVerifyStatus(int status)
	{
		return status switch
		{
			(int)WalletRechargeVerifyStatus.Processing => "待处理",
			(int)WalletRechargeVerifyStatus.Success => "成功",
			(int)WalletRechargeVerifyStatus.Fail => "失败"
		};
	}
}