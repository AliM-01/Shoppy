using BM.Application.Contracts.ArticleCategory.DTOs;
using BM.Application.Contracts.ArticleCategory.Queries;

namespace BM.Application.ArticleCategory.QueryHandles;
public class GetArticleCategoriesSelectListQueryHandler : IRequestHandler<GetArticleCategoriesSelectListQuery, IEnumerable<ArticleCategoryForSelectListDto>>
{
    #region Ctor

    private readonly IRepository<Domain.ArticleCategory.ArticleCategory> _articleCategoryRepository;
    private readonly IMapper _mapper;

    public GetArticleCategoriesSelectListQueryHandler(IRepository<Domain.ArticleCategory.ArticleCategory> articleCategoryRepository, IMapper mapper)
    {
        _articleCategoryRepository = Guard.Against.Null(articleCategoryRepository, nameof(_articleCategoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public Task<IEnumerable<ArticleCategoryForSelectListDto>> Handle(GetArticleCategoriesSelectListQuery request, CancellationToken cancellationToken)
    {
        var categories = _articleCategoryRepository
                                                        .AsQueryable(cancellationToken: cancellationToken)
                                                        .OrderByDescending(p => p.LastUpdateDate)
                                                        .ToList()
                                                        .Select(article => new ArticleCategoryForSelectListDto
                                                        {
                                                            Id = article.Id,
                                                            Title = article.Title
                                                        })
                                                        .ToList();

        if (categories is null)
            throw new NotFoundApiException();

        return Task.FromResult((IEnumerable<ArticleCategoryForSelectListDto>)categories);
    }
}