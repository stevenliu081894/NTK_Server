/// <summary>
/// 1--买 2--卖
/// </summary>
public enum DirType
{
	Buy = 1,
	Sell = 2
}

/// <summary>
/// 状态 0.审核中 1.成功，2.失败
/// </summary>
public enum TradeMoneyCheckStateEnum
{
	Processing = 0,
	Success = 1,
	Fail = 2
}


public static class TradeMoneyCheckStateConvertEnum
{
    public static string TradeMoneyCheckState(int status)
    {
        return status switch
        {
            (int)TradeMoneyCheckStateEnum.Processing => "审核中",
            (int)TradeMoneyCheckStateEnum.Success => "成功",
            (int)TradeMoneyCheckStateEnum.Fail => "失败",
        };
    }
}