using BM.Application.Contracts.ArticleCategory.DTOs;
using BM.Application.Contracts.ArticleCategory.Queries;

namespace BM.Application.ArticleCategory.QueryHandles;
public class GetArticleCategoriesSelectListQueryHandler : IRequestHandler<GetArticleCategoriesSelectListQuery, Response<IEnumerable<ArticleCategoryForSelectListDto>>>
{
    #region Ctor

    private readonly IMongoHelper<Domain.ArticleCategory.ArticleCategory> _articleCategoryHelper;
    private readonly IMapper _mapper;

    public GetArticleCategoriesSelectListQueryHandler(IMongoHelper<Domain.ArticleCategory.ArticleCategory> articleCategoryHelper, IMapper mapper)
    {
        _articleCategoryHelper = Guard.Against.Null(articleCategoryHelper, nameof(_articleCategoryHelper));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<IEnumerable<ArticleCategoryForSelectListDto>>> Handle(GetArticleCategoriesSelectListQuery request, CancellationToken cancellationToken)
    {
        var categories = (await
            _articleCategoryHelper
            .AsQueryable()
            .OrderByDescending(p => p.LastUpdateDate)
            .ToListAsyncSafe()
            )
            .Select(Article => new ArticleCategoryForSelectListDto
            {
                Id = Article.Id,
                Title = Article.Title
            })
            .ToList();

        if (categories is null)
            throw new NotFoundApiException();

        return new Response<IEnumerable<ArticleCategoryForSelectListDto>>(categories);
    }
}