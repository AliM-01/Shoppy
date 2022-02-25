using _01_Shoppy.Query.Models.Blog.ArticleCategory;
using _01_Shoppy.Query.Models.ProductCategory;

namespace _01_Shoppy.Query.Models.Common;

public class MenuDto
{
    public MenuDto(List<ProductCategoryQueryModel> productCategories, List<ArticleCategoryQueryModel> articleCategories)
    {
        ProductCategories = productCategories;
        ArticleCategories = articleCategories;
    }

    public List<ProductCategoryQueryModel> ProductCategories { get; set; }

    public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
}
