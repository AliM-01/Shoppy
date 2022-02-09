using BM.Application.Contracts.ArticleCategory.Queries;

namespace BM.Infrastructure.Shared.Validators.ArticleCategory;

public class FilterArticleCategoriesQueryValidator : AbstractValidator<FilterArticleCategoriesQuery>
{
    public FilterArticleCategoriesQueryValidator()
    {
        RuleFor(p => p.Filter.Title)
            .MaxLengthValidator("عنوان", 100);
    }
}

