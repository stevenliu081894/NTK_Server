using System.ComponentModel.DataAnnotations;
/// <summary>
/// 接收对象
/// </summary>
public enum AccountTypeEnum
{
    [Display(Name = "无")]
    Unset = 0,
    [Display(Name = "会员")]
    Member = 1,
    [Display(Name = "管理员")]
    AdminUser = 2,
    [Display(Name = "运营商")]
    Agent = 3,
}

/// <summary>
/// 读取状态
/// </summary>
public enum MessageReadStatusEnum
{
    [Display(Name = "Unread")]
    Unread = 0,
    [Display(Name = "Read")]
    Read = 1
}

/// <summary>
/// 发送状态
/// </summary>
public enum MessageSendStatus
{
    [Display(Name = "Draft")]
    Draft = 0,
    [Display(Name = "Sented")]
    Sented = 1,
    [Display(Name = "Failed")]
    Failed = 2
}

/// <summary>
/// 发送方法
/// </summary>
public enum MessageTransTypeEnum
{
    [Display(Name = "Internal")]
    Internal = 1,
    [Display(Name = "email")]
    Email = 2,
    [Display(Name = "SMS")]
    Sms = 3
}

public enum MessageTransEnum
{
    [Display(Name = "Unread")]
    Sys = 1,
    [Display(Name = "Read")]
    User = 2
}
