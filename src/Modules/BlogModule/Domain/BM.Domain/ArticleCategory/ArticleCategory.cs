using _0_Framework.Application.Attributes;
using _0_Framework.Domain;
using _0_Framework.Domain.Seo;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BM.Domain.ArticleCategory;

public class ArticleCategory : SeoPropertiesForDomainModels
{
    #region Properties

    [Display(Name = "عنوان")]
    public string Title { get; set; }

    [Display(Name = "توضیحات")]
    public string Description { get; set; }

    [Display(Name = "ترتیب نمایش")]
    public int OrderShow { get; set; }

    [Display(Name = "تصویر")]
    [JsonProperty("imageFile")]
    [MaxFileSize((3 * 1024 * 1024), ErrorMessage = DomainErrorMessage.FileMaxSizeMessage)]
    public IFormFile ImageFile { get; set; }

    [Display(Name = "عنوان لینک")]
    public string Slug { get; set; }

    [Display(Name = "عنوان لینک")]
    public string CanonicalAddress { get; set; }

    #endregion 
}
