using BM.Application.Contracts.Article.DTOs;
using BM.Application.Contracts.Article.Queries;

namespace BM.Application.Article.QueryHandles;
public class GetArticleDetailsQueryHandler : IRequestHandler<GetArticleDetailsQuery, Response<EditArticleDto>>
{
    #region Ctor

    private readonly IBlogDbContext _blogContext;
    private readonly IMapper _mapper;

    public GetArticleDetailsQueryHandler(IBlogDbContext blogContext, IMapper mapper)
    {
        _blogContext = Guard.Against.Null(blogContext, nameof(_blogContext));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<EditArticleDto>> Handle(GetArticleDetailsQuery request, CancellationToken cancellationToken)
    {
        var article = await _blogContext.Articles.FindAsync(filter: x => x.Id == request.Id);

        if (article is null)
            throw new NotFoundApiException();

        var mappedArticle = _mapper.Map<EditArticleDto>(article);

        return new Response<EditArticleDto>(mappedArticle);
    }
}