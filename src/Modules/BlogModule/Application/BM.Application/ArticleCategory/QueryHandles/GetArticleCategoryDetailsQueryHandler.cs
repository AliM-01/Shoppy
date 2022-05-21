using BM.Application.Contracts.ArticleCategory.DTOs;
using BM.Application.Contracts.ArticleCategory.Queries;

namespace BM.Application.ArticleCategory.QueryHandles;
public class GetArticleCategoryDetailsQueryHandler : IRequestHandler<GetArticleCategoryDetailsQuery, EditArticleCategoryDto>
{
    #region Ctor

    private readonly IRepository<Domain.ArticleCategory.ArticleCategory> _articleCategoryRepository;
    private readonly IMapper _mapper;

    public GetArticleCategoryDetailsQueryHandler(IRepository<Domain.ArticleCategory.ArticleCategory> articleCategoryRepository, IMapper mapper)
    {
        _articleCategoryRepository = Guard.Against.Null(articleCategoryRepository, nameof(_articleCategoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<EditArticleCategoryDto> Handle(GetArticleCategoryDetailsQuery request, CancellationToken cancellationToken)
    {
        var articleCategory = await _articleCategoryRepository.FindByIdAsync(request.Id, cancellationToken);

        NotFoundApiException.ThrowIfNull(articleCategory);

        return _mapper.Map<EditArticleCategoryDto>(articleCategory);
    }
}