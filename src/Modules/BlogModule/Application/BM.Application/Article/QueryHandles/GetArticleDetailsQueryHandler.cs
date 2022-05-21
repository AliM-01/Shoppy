using BM.Application.Contracts.Article.DTOs;
using BM.Application.Contracts.Article.Queries;

namespace BM.Application.Article.QueryHandles;
public class GetArticleDetailsQueryHandler : IRequestHandler<GetArticleDetailsQuery, EditArticleDto>
{
    #region Ctor

    private readonly IRepository<Domain.Article.Article> _articleRepository;
    private readonly IMapper _mapper;

    public GetArticleDetailsQueryHandler(IRepository<Domain.Article.Article> articleRepository, IMapper mapper)
    {
        _articleRepository = Guard.Against.Null(articleRepository, nameof(_articleRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<EditArticleDto> Handle(GetArticleDetailsQuery request, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.FindByIdAsync(request.Id, cancellationToken);

        if (article is null)
            throw new NotFoundApiException();

        return _mapper.Map<EditArticleDto>(article);
    }
}