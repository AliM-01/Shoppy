using BM.Application.Contracts.ArticleCategory.DTOs;
using BM.Application.Contracts.ArticleCategory.Queries;

namespace BM.Application.ArticleCategory.QueryHandles;
public class GetArticleCategoryDetailsQueryHandler : IRequestHandler<GetArticleCategoryDetailsQuery, Response<EditArticleCategoryDto>>
{
    #region Ctor

    private readonly IBlogDbContext _blogContext;
    private readonly IMapper _mapper;

    public GetArticleCategoryDetailsQueryHandler(IBlogDbContext blogContext, IMapper mapper)
    {
        _blogContext = Guard.Against.Null(blogContext, nameof(_blogContext));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<EditArticleCategoryDto>> Handle(GetArticleCategoryDetailsQuery request, CancellationToken cancellationToken)
    {
        var articleCategory = (
            await _blogContext.ArticleCategories.FindAsync(MongoDbFilters<Domain.ArticleCategory.ArticleCategory>.GetByIdFilter(request.Id))
            ).FirstOrDefault();

        if (articleCategory is null)
            throw new NotFoundApiException();

        var mappedArticleCategory = _mapper.Map<EditArticleCategoryDto>(articleCategory);

        return new Response<EditArticleCategoryDto>(mappedArticleCategory);
    }
}