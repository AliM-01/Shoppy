using _01_Shoppy.Query.Models.Blog.ArticleCategory;
using _01_Shoppy.Query.Models.ProductCategory;

namespace _01_Shoppy.Query.Models.Common;

public class MenuDto
{
    public List<ProductCategoryQueryModel> ProductCategories { get; set; }

    public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
}
