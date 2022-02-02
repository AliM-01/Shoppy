using System.ComponentModel.DataAnnotations;

namespace _0_Framework.Domain.Seo;

public class ImagePropertiesForDomainModels : BaseEntity
{
    [Display(Name = "جزییات تصویر")]
    public string ImageAlt { get; set; }

    [Display(Name = "عنوان تصویر")]
    public string ImageTitle { get; set; }
}
