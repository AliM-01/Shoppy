using _0_Framework.Application.Models.Paging;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AM.Application.Contracts.Account.DTOs;

public class FilterAccountDto : BasePaging
{
    #region Properties

    [Display(Name = "نام و نام خانوادگی")]
    [JsonProperty("fullName")]
    public string FullName { get; set; }

    [JsonProperty("mobile")]
    public string Mobile { get; set; }

    [JsonProperty("accounts")]
    public IEnumerable<AccountDto> Accounts { get; set; }

    #endregion

    #region Methods

    public FilterAccountDto SetData(IEnumerable<AccountDto> account)
    {
        this.Accounts = account;
        return this;
    }

    public FilterAccountDto SetPaging(BasePaging paging)
    {
        this.PageId = paging.PageId;
        this.AllPagesCount = paging.AllPagesCount;
        this.StartPage = paging.StartPage;
        this.EndPage = paging.EndPage;
        this.ShownPages = paging.ShownPages;
        this.SkipPage = paging.SkipPage;
        this.TakePage = paging.TakePage;
        this.PageCount = paging.PageCount;
        return this;
    }

    #endregion
}