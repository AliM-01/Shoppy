using BM.Application.Contracts.ArticleCategory.DTOs;
using BM.Application.Contracts.ArticleCategory.Queries;
using Microsoft.EntityFrameworkCore;

namespace BM.Application.ArticleCategory.QueryHandles;
public class GetArticleCategoriesSelectListQueryHandler : IRequestHandler<GetArticleCategoriesSelectListQuery, Response<IEnumerable<ArticleCategoryForSelectListDto>>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ArticleCategory.ArticleCategory> _articleCategoryRepository;
    private readonly IMapper _mapper;

    public GetArticleCategoriesSelectListQueryHandler(IGenericRepository<Domain.ArticleCategory.ArticleCategory> articleCategoryRepository, IMapper mapper)
    {
        _articleCategoryRepository = Guard.Against.Null(articleCategoryRepository, nameof(_articleCategoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<IEnumerable<ArticleCategoryForSelectListDto>>> Handle(GetArticleCategoriesSelectListQuery request, CancellationToken cancellationToken)
    {
        var categories = await
            _articleCategoryRepository.GetQuery()
            .OrderByDescending(p => p.LastUpdateDate)
            .Select(Article => new ArticleCategoryForSelectListDto
            {
                Id = Article.Id,
                Title = Article.Title
            })
            .ToListAsync(cancellationToken);

        if (categories is null)
            throw new NotFoundApiException();

        return new Response<IEnumerable<ArticleCategoryForSelectListDto>>(categories);
    }
}