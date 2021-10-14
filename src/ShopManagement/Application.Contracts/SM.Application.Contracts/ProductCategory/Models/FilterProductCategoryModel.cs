using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application.Models.Paging;
using _0_Framework.Domain;

namespace SM.Application.Contracts.ProductCategory.Models
{
    public class FilterProductCategoryModel : BasePaging
    {
        #region Properties

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
        [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
        public string Title { get; set; }

        public List<ProductCategoryViewModel> Products { get; set; }

        #endregion Properties

        #region Methods

        public FilterProductCategoryModel SetEntities(List<ProductCategoryViewModel> product)
        {
            this.Products = product;
            return this;
        }

        public FilterProductCategoryModel SetPaging(BasePaging paging)
        {
            this.PageId = paging.PageId;
            this.AllPagesCount = paging.AllPagesCount;
            this.StartPage = paging.StartPage;
            this.EndPage = paging.EndPage;
            this.ShownPages = paging.ShownPages;
            this.TakePage = paging.TakePage;
            this.SkipPage = paging.SkipPage;
            this.PageCount = paging.PageCount;
            return this;
        }

        #endregion

    }
}