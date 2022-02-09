using BM.Application.Contracts.Article.DTOs;
using BM.Application.Contracts.Article.Queries;

namespace BM.Application.Article.QueryHandles;
public class GetArticleDetailsQueryHandler : IRequestHandler<GetArticleDetailsQuery, Response<EditArticleDto>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Article.Article> _articleRepository;
    private readonly IMapper _mapper;

    public GetArticleDetailsQueryHandler(IGenericRepository<Domain.Article.Article> articleRepository, IMapper mapper)
    {
        _articleRepository = Guard.Against.Null(articleRepository, nameof(_articleRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<EditArticleDto>> Handle(GetArticleDetailsQuery request, CancellationToken cancellationToken)
    {
        var Article = await _articleRepository.GetEntityById(request.Id);

        if (Article is null)
            throw new NotFoundApiException();

        var mappedArticle = _mapper.Map<EditArticleDto>(Article);

        return new Response<EditArticleDto>(mappedArticle);
    }
}