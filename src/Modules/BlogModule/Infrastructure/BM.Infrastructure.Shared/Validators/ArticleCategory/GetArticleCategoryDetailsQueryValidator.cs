using BM.Application.Contracts.ArticleCategory.Queries;

namespace BM.Infrastructure.Shared.Validators.ArticleCategory;

public class GetArticleCategoryDetailsQueryValidator : AbstractValidator<GetArticleCategoryDetailsQuery>
{
    public GetArticleCategoryDetailsQueryValidator()
    {
        RuleFor(p => p.Id)
            .RangeValidator("شناسه دسته بندی", 1, 1000);
    }
}
