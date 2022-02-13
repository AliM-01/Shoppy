namespace _01_Shoppy.Query.Contracts.Blog.Article;

internal interface IArticleQuery
{
    Task<Response<List<ArticleQueryModel>>> GetLatestArticles();
}
