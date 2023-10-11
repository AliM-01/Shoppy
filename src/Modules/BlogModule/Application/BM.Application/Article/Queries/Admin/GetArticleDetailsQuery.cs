using BM.Application.Article.DTOs;
using FluentValidation;

namespace BM.Application.Article.Queries.Admin;

public record GetArticleDetailsAdminQuery(string Id) : IRequest<EditArticleDto>;

public class GetArticleDetailsAdminQueryValidator : AbstractValidator<GetArticleDetailsAdminQuery>
{
    public GetArticleDetailsAdminQueryValidator()
    {
        RuleFor(p => p.Id)
            .RequiredValidator("شناسه دسته بندی");
    }
}

public class GetArticleDetailsAdminQueryHandler : IRequestHandler<GetArticleDetailsAdminQuery, EditArticleDto>
{
    private readonly IRepository<Domain.Article.Article> _articleRepository;
    private readonly IMapper _mapper;

    public GetArticleDetailsAdminQueryHandler(IRepository<Domain.Article.Article> articleRepository, IMapper mapper)
    {
        _articleRepository = Guard.Against.Null(articleRepository, nameof(_articleRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<EditArticleDto> Handle(GetArticleDetailsAdminQuery request, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.FindByIdAsync(request.Id, cancellationToken);

        NotFoundApiException.ThrowIfNull(article);

        return _mapper.Map<EditArticleDto>(article);
    }
}