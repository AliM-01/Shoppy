using System.ComponentModel.DataAnnotations;
using _0_Framework.Domain;

namespace SM.Domain.ProductCategory
{
    public class ProductCategory : BaseEntity
    {
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "تصویر")]
        public string ImagePath { get; set; }

        [Display(Name = "جزییات تصویر")]
        public string ImageAlt { get; set; }

        [Display(Name = "عنوان تصویر")]
        public string ImageTitle { get; set; }

        [Display(Name = "کلمات کلیدی")]
        public string MetaKeywords { get; set; }

        [Display(Name = "توضیحات Meta")]
        public string MetaDescription { get; set; }

        [Display(Name = "عنوان لینک")]
        public string Slug { get; set; }
    }
}