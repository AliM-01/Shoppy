using System.ComponentModel.DataAnnotations;
using _0_Framework.Domain;

namespace SM.Application.Contracts.ProductCategory.Models
{
    public class FilterProductCategoryModel
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
        [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
        public string Title { get; set; }
    }
}