using BM.Application.Contracts.Article.Queries;

namespace BM.Infrastructure.Shared.Validators.Article.Queries;

public class GetArticleDetailsQueryValidator : AbstractValidator<GetArticleDetailsQuery>
{
    public GetArticleDetailsQueryValidator()
    {
        RuleFor(p => p.Id)
            .RangeValidator("شناسه دسته بندی", 1, 1000);
    }
}
