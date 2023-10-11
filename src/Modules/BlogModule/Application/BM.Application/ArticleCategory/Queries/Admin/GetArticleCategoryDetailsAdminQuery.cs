using BM.Application.ArticleCategory.Models;
using BM.Application.ArticleCategory.Models.Admin;
using FluentValidation;

namespace BM.Application.ArticleCategory.Queries.Admin;

public record GetArticleCategoryDetailsAdminQuery(string Id) : IRequest<EditArticleCategoryAdminDto>;

public class GetArticleCategoryDetailsQueryValidator : AbstractValidator<GetArticleCategoryDetailsAdminQuery>
{
    public GetArticleCategoryDetailsQueryValidator()
    {
        RuleFor(p => p.Id)
            .RequiredValidator("شناسه دسته بندی");
    }
}

public class GetArticleCategoryDetailsAdminQueryHandler : IRequestHandler<GetArticleCategoryDetailsAdminQuery, EditArticleCategoryAdminDto>
{
    private readonly IRepository<Domain.ArticleCategory.ArticleCategory> _articleCategoryRepository;
    private readonly IMapper _mapper;

    public GetArticleCategoryDetailsAdminQueryHandler(IRepository<Domain.ArticleCategory.ArticleCategory> articleCategoryRepository, IMapper mapper)
    {
        _articleCategoryRepository = Guard.Against.Null(articleCategoryRepository, nameof(_articleCategoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<EditArticleCategoryAdminDto> Handle(GetArticleCategoryDetailsAdminQuery request, CancellationToken cancellationToken)
    {
        var articleCategory = await _articleCategoryRepository.FindByIdAsync(request.Id, cancellationToken);

        NotFoundApiException.ThrowIfNull(articleCategory);

        return _mapper.Map<EditArticleCategoryAdminDto>(articleCategory);
    }
}