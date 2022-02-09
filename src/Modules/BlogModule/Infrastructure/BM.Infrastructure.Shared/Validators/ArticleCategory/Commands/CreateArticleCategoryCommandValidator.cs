using BM.Application.Contracts.ArticleCategory.Commands;

namespace BM.Infrastructure.Shared.Validators.ArticleCategory;

public class CreateArticleCategoryCommandValidator : AbstractValidator<CreateArticleCategoryCommand>
{
    public CreateArticleCategoryCommandValidator()
    {
        RuleFor(p => p.ArticleCategory.Title)
            .RequiredValidator("عنوان دسته بندی");

        RuleFor(p => p.ArticleCategory.Description)
            .RequiredValidator("توضیحات دسته بندی")
            .MaxLengthValidator("توضیحات دسته بندی", 250);

        RuleFor(p => p.ArticleCategory.OrderShow)
            .RangeValidator("اولویت نمایش", 1, 1000);

        RuleFor(p => p.ArticleCategory.ImageFile)
            .MaxFileSizeValidator((3 * 1024));
    }
}
