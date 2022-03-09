using System.Runtime.Serialization;

namespace OM.Application.Contracts.Order.Enums;

public enum FilterOrderPaymentStatus
{
    [Display(Name = "همه")]
    [EnumMember(Value = "همه")]
    All,
    [Display(Name = "پرداخت شده")]
    [EnumMember(Value = "پرداخت شده")]
    IsPaid,
    [Display(Name = "منتظر پرداخت")]
    [EnumMember(Value = "منتظر پرداخت")]
    PaymenyPending,
    [Display(Name = "کنسل شده")]
    [EnumMember(Value = "کنسل شده")]
    IsCanceled,
}