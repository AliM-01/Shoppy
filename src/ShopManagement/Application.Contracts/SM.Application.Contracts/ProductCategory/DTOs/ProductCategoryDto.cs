using System.ComponentModel.DataAnnotations;

namespace SM.Application.Contracts.ProductCategory.Models
{
    public class ProductCategoryDto
    {
        public long Id { get; set; }

        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "تصویر")]
        public string ImagePath { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public string CreationDate { get; set; }

        [Display(Name = "محصولات با این دسته بندی")]
        public long ProductsCount { get; set; }
    }
}