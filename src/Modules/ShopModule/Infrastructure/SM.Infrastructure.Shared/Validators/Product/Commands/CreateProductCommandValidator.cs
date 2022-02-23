using SM.Application.Contracts.Product.Commands;

namespace SM.Infrastructure.Shared.Validators.Product.Commands;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.Product.CategoryId)
            .RequiredValidator("شناسه دسته بندی");

        RuleFor(p => p.Product.Title)
            .RequiredValidator("عنوان")
            .MaxLengthValidator("عنوان", 100);

        RuleFor(p => p.Product.ShortDescription)
            .RequiredValidator("توضیحات کوتاه")
            .MaxLengthValidator("توضیحات کوتاه", 250);

        RuleFor(p => p.Product.Description)
            .RequiredValidator("توضیحات");

        RuleFor(p => p.Product.ImageFile)
            .MaxFileSizeValidator((3 * 1024 * 1024));
    }
}

