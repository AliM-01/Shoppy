using BM.Application.Contracts.ArticleCategory.DTOs;
using BM.Application.Contracts.ArticleCategory.Queries;

namespace BM.Application.ArticleCategory.QueryHandles;
public class GetArticleCategoryDetailsQueryHandler : IRequestHandler<GetArticleCategoryDetailsQuery, Response<EditArticleCategoryDto>>
{
    #region Ctor

    private readonly IMongoHelper<Domain.ArticleCategory.ArticleCategory> _articleCategoryHelper;
    private readonly IMapper _mapper;

    public GetArticleCategoryDetailsQueryHandler(IMongoHelper<Domain.ArticleCategory.ArticleCategory> articleCategoryHelper, IMapper mapper)
    {
        _articleCategoryHelper = Guard.Against.Null(articleCategoryHelper, nameof(_articleCategoryHelper));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<EditArticleCategoryDto>> Handle(GetArticleCategoryDetailsQuery request, CancellationToken cancellationToken)
    {
        var articleCategory = await _articleCategoryHelper.GetByIdAsync(request.Id);

        if (articleCategory is null)
            throw new NotFoundApiException();

        var mappedArticleCategory = _mapper.Map<EditArticleCategoryDto>(articleCategory);

        return new Response<EditArticleCategoryDto>(mappedArticleCategory);
    }
}