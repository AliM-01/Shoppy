using BM.Application.ArticleCategory.Models;
using BM.Application.ArticleCategory.Models.Site;

namespace BM.Application.ArticleCategory.Queries.SiteCategory;

public record GetArticleCategorySiteListQuery() : IRequest<IEnumerable<ArticleCategorySiteDto>>;

public class GetArticleCategorySiteListQueryHandler : IRequestHandler<GetArticleCategorySiteListQuery, IEnumerable<ArticleCategorySiteDto>>
{
    private readonly IRepository<BM.Domain.ArticleCategory.ArticleCategory> _articleCategoryRepository;
    private readonly IMapper _mapper;

    public GetArticleCategorySiteListQueryHandler(
        IRepository<BM.Domain.ArticleCategory.ArticleCategory> articleCategoryRepository, IMapper mapper)
    {
        _articleCategoryRepository = Guard.Against.Null(articleCategoryRepository, nameof(_articleCategoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public Task<IEnumerable<ArticleCategorySiteDto>> Handle(GetArticleCategorySiteListQuery request, CancellationToken cancellationToken)
    {
        var articleCategories =
            _articleCategoryRepository
            .AsQueryable(cancellationToken: cancellationToken)
            .ToList()
            .Select(articleCategory => _mapper.Map(articleCategory, new ArticleCategorySiteDto()))
            .ToList();

        return Task.FromResult((IEnumerable<ArticleCategorySiteDto>)articleCategories);
    }
}
