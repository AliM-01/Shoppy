using _0_Framework.Application.Exceptions;
using _0_Framework.Infrastructure.IRepository;
using Ardalis.GuardClauses;
using BM.Domain.Article;
using CM.Application.Contracts.Sevices;

namespace CM.Infrastructure.ArticleAcl;

public class CMArticleAclService : ICMArticleAcl
{
    #region ctor

    private readonly IRepository<Article> _articleRepository;

    public CMArticleAclService(IRepository<Article> articleRepository)
    {
        _articleRepository = Guard.Against.Null(articleRepository, nameof(_articleRepository));

    }

    #endregion

    #region ExistsArticle

    public async Task<bool> ExistsArticle(string articleId)
    {
        return await _articleRepository.ExistsAsync(a => a.Id == articleId);
    }

    #endregion



    #region GetProductTitle

    public async Task<string> GetArticleTitle(string articleId)
    {
        if (!(await ExistsArticle(articleId)))
            throw new NotFoundApiException("مقاله ای با این شناسه پیدا نشد");

        return (await _articleRepository.FindByIdAsync(articleId)).Title;
    }
    #endregion
}
