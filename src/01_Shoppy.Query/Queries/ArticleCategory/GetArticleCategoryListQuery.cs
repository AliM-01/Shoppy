using _0_Framework.Infrastructure;
using _01_Shoppy.Query.Models.Blog.ArticleCategory;

namespace _01_Shoppy.Query.Queries.ArticleCategory;

public record GetArticleCategoryListQuery() : IRequest<Response<IEnumerable<ArticleCategoryQueryModel>>>;

public class GetArticleCategoryListQueryHandler : IRequestHandler<GetArticleCategoryListQuery, Response<IEnumerable<ArticleCategoryQueryModel>>>
{
    #region Ctor

    private readonly IGenericRepository<BM.Domain.ArticleCategory.ArticleCategory> _articleCategoryRepository;
    private readonly IMapper _mapper;

    public GetArticleCategoryListQueryHandler(
        IGenericRepository<BM.Domain.ArticleCategory.ArticleCategory> articleCategoryRepository, IMapper mapper)
    {
        _articleCategoryRepository = Guard.Against.Null(articleCategoryRepository, nameof(_articleCategoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<IEnumerable<ArticleCategoryQueryModel>>> Handle(GetArticleCategoryListQuery request, CancellationToken cancellationToken)
    {
        var articleCategories = (await
            _articleCategoryRepository.AsQueryable(cancellationToken: cancellationToken)
            .ToListAsyncSafe()
            )
            .Select(articleCategory => _mapper.Map(articleCategory, new ArticleCategoryQueryModel()))
            .ToList();

        return new Response<IEnumerable<ArticleCategoryQueryModel>>(articleCategories);
    }
}
