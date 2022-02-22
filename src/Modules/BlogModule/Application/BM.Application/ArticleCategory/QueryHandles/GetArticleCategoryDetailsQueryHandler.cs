using BM.Application.Contracts.ArticleCategory.DTOs;
using BM.Application.Contracts.ArticleCategory.Queries;

namespace BM.Application.ArticleCategory.QueryHandles;
public class GetArticleCategoryDetailsQueryHandler : IRequestHandler<GetArticleCategoryDetailsQuery, Response<EditArticleCategoryDto>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ArticleCategory.ArticleCategory> _articleCategoryRepository;
    private readonly IMapper _mapper;

    public GetArticleCategoryDetailsQueryHandler(IGenericRepository<Domain.ArticleCategory.ArticleCategory> articleCategoryRepository, IMapper mapper)
    {
        _articleCategoryRepository = Guard.Against.Null(articleCategoryRepository, nameof(_articleCategoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<EditArticleCategoryDto>> Handle(GetArticleCategoryDetailsQuery request, CancellationToken cancellationToken)
    {
        var articleCategory = await _articleCategoryRepository.GetByIdAsync(request.Id);

        if (articleCategory is null)
            throw new NotFoundApiException();

        var mappedArticleCategory = _mapper.Map<EditArticleCategoryDto>(articleCategory);

        return new Response<EditArticleCategoryDto>(mappedArticleCategory);
    }
}