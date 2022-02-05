using _0_Framework.Domain.Validators;
using BM.Application.Contracts.ArticleCategory.Commands;

namespace BM.Infrastructure.Shared.Validators.ArticleCategory;

public class CreateArticleCategoryValidator : AbstractValidator<CreateArticleCategoryCommand>
{
    public CreateArticleCategoryValidator()
    {
        RuleFor(p => p.ArticleCategory.Title).RequiredValidator("عنوان دسته بندی");

        RuleFor(p => p.ArticleCategory.Description)
            .RequiredValidator("عنوان دسته بندی")
            .MaxLengthValidator("عنوان دسته بندی", 250);

        RuleFor(p => p.ArticleCategory.OrderShow)
            .RangeValidator("اولویت نمایش", 1, 1000);

        RuleFor(p => p.ArticleCategory.ImageFile)
            .MaxFileSizeValidator((3 * 1024));
    }
}
