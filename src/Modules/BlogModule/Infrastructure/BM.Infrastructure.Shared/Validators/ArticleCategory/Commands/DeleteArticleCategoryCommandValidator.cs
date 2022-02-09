using BM.Application.Contracts.ArticleCategory.Commands;

namespace BM.Infrastructure.Shared.Validators.ArticleCategory;

public class DeleteArticleCategoryCommandValidator : AbstractValidator<DeleteArticleCategoryCommand>
{
    public DeleteArticleCategoryCommandValidator()
    {
        RuleFor(p => p.ArticleCategoryId)
            .RangeValidator("شناسه دسته بندی", 1, 1000);
    }
}

