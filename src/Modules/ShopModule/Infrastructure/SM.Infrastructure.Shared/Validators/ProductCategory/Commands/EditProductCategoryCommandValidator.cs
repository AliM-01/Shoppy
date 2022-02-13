using SM.Application.Contracts.ProductCategory.Commands;

namespace SM.Infrastructure.Shared.Validators.ProductCategory.Commands;

public class EditProductCategoryCommandValidator : AbstractValidator<EditProductCategoryCommand>
{
    public EditProductCategoryCommandValidator()
    {
        RuleFor(p => p.ProductCategory.Id)
            .RangeValidator("شناسه", 1, 100000);

        RuleFor(p => p.ProductCategory.Title)
            .RequiredValidator("عنوان")
            .MaxLengthValidator("عنوان", 100);

        RuleFor(p => p.ProductCategory.Description)
            .RequiredValidator("توضیحات")
            .MaxLengthValidator("توضیحات", 250);

        RuleFor(p => p.ProductCategory.ImageFile)
            .MaxFileSizeValidator((3 * 1024 * 1024), false);
    }
}

