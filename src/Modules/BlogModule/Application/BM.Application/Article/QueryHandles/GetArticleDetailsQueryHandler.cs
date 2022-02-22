using BM.Application.Contracts.Article.DTOs;
using BM.Application.Contracts.Article.Queries;

namespace BM.Application.Article.QueryHandles;
public class GetArticleDetailsQueryHandler : IRequestHandler<GetArticleDetailsQuery, Response<EditArticleDto>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Article.Article> _articleHelper;
    private readonly IMapper _mapper;

    public GetArticleDetailsQueryHandler(IGenericRepository<Domain.Article.Article> articleHelper, IMapper mapper)
    {
        _articleHelper = Guard.Against.Null(articleHelper, nameof(_articleHelper));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<EditArticleDto>> Handle(GetArticleDetailsQuery request, CancellationToken cancellationToken)
    {
        var article = await _articleHelper.GetByIdAsync(request.Id);

        if (article is null)
            throw new NotFoundApiException();

        var mappedArticle = _mapper.Map<EditArticleDto>(article);

        return new Response<EditArticleDto>(mappedArticle);
    }
}