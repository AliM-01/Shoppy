using BM.Application.Contracts.ArticleCategory.DTOs;
using BM.Application.Contracts.ArticleCategory.Queries;

namespace BM.Application.ArticleCategory.QueryHandles;
public class GetArticleCategoriesSelectListQueryHandler : IRequestHandler<GetArticleCategoriesSelectListQuery, Response<IEnumerable<ArticleCategoryForSelectListDto>>>
{
    #region Ctor

    private readonly IBlogDbContext _blogContext;
    private readonly IMapper _mapper;

    public GetArticleCategoriesSelectListQueryHandler(IBlogDbContext blogContext, IMapper mapper)
    {
        _blogContext = Guard.Against.Null(blogContext, nameof(_blogContext));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<IEnumerable<ArticleCategoryForSelectListDto>>> Handle(GetArticleCategoriesSelectListQuery request, CancellationToken cancellationToken)
    {
        var categories = (await
            _blogContext.ArticleCategories
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