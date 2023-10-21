using SM.Application.ProductCategory.Commands;

namespace SM.Infrastructure.Shared.Validators.ProductCategory.Commands;

public class CreateProductCategoryCommandValidator : AbstractValidator<CreateProductCategoryCommand>
{
    public CreateProductCategoryCommandValidator()
    {
        RuleFor(p => p.ProductCategory.Title)
            .RequiredValidator("عنوان")
            .MaxLengthValidator("عنوان", 100);

        RuleFor(p => p.ProductCategory.Description)
            .RequiredValidator("توضیحات")
            .MaxLengthValidator("توضیحات", 250);

        RuleFor(p => p.ProductCategory.ImageFile)
            .MaxFileSizeValidator((3 * 1024 * 1024));
    }
}

