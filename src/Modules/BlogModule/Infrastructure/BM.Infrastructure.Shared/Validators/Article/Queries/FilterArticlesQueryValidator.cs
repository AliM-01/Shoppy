using BM.Application.Contracts.Article.Queries;

namespace BM.Infrastructure.Shared.Validators.Article.Queries;

public class FilterArticlesQueryValidator : AbstractValidator<FilterArticlesQuery>
{
    public FilterArticlesQueryValidator()
    {
        RuleFor(p => p.Filter.Title)
            .MaxLengthValidator("عنوان", 100);

        RuleFor(p => p.Filter.CategoryId)
            .RequiredValidator("شناسه دسته بندی");
    }
}

