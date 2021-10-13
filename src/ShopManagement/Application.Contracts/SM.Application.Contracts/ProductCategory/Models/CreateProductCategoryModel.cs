﻿using System.ComponentModel.DataAnnotations;
using _0_Framework.Domain;

namespace SM.Application.Contracts.ProductCategory.Models
{
    public class CreateProductCategoryModel
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
        [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
        [MaxLength(200, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
        public string Description { get; set; }

        [Display(Name = "تصویر")]
        [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
        public string ImagePath { get; set; }

        [Display(Name = "جزییات تصویر")]
        [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
        [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
        public string ImageAlt { get; set; }

        [Display(Name = "عنوان تصویر")]
        [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
        [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
        public string ImageTitle { get; set; }

        [Display(Name = "کلمات کلیدی")]
        [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
        [MaxLength(80, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
        public string MetaKeywords { get; set; }

        [Display(Name = "توضیحات Meta")]
        [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
        [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
        public string MetaDescription { get; set; }
    }
}