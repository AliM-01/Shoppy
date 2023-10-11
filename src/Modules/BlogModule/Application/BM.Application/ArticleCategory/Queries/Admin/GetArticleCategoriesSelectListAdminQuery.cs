using BM.Application.ArticleCategory.Models;
using BM.Application.ArticleCategory.Models.Admin;

namespace BM.Application.ArticleCategory.Queries.Admin;

public record GetArticleCategoriesSelectListAdminQuery : IRequest<IEnumerable<ArticleCategoryForSelectListAdminDto>>;

public class GetArticleCategoriesSelectListAdminQueryHandler : IRequestHandler<GetArticleCategoriesSelectListAdminQuery, IEnumerable<ArticleCategoryForSelectListAdminDto>>
{
    private readonly IRepository<Domain.ArticleCategory.ArticleCategory> _articleCategoryRepository;
    private readonly IMapper _mapper;

    public GetArticleCategoriesSelectListAdminQueryHandler(IRepository<Domain.ArticleCategory.ArticleCategory> articleCategoryRepository, IMapper mapper)
    {
        _articleCategoryRepository = Guard.Against.Null(articleCategoryRepository, nameof(_articleCategoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public Task<IEnumerable<ArticleCategoryForSelectListAdminDto>> Handle(GetArticleCategoriesSelectListAdminQuery request, CancellationToken cancellationToken)
    {
        var categories = _articleCategoryRepository
                                                        .AsQueryable(cancellationToken: cancellationToken)
                                                        .OrderByDescending(p => p.LastUpdateDate)
                                                        .ToList()
                                                        .Select(article => new ArticleCategoryForSelectListAdminDto
                                                        {
                                                            Id = article.Id,
                                                            Title = article.Title
                                                        })
                                                        .ToList();

        NotFoundApiException.ThrowIfNull(categories);

        return Task.FromResult((IEnumerable<ArticleCategoryForSelectListAdminDto>)categories);
    }
}