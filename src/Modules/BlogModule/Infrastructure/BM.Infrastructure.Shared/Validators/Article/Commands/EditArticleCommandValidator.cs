using BM.Application.Contracts.Article.Commands;

namespace BM.Infrastructure.Shared.Validators.Article.Commands;

public class EditArticleCommandValidator : AbstractValidator<EditArticleCommand>
{
    public EditArticleCommandValidator()
    {
        RuleFor(p => p.Article.Id)
            .RangeValidator("شناسه", 1, 1000);

        RuleFor(p => p.Article.Title)
            .RequiredValidator("عنوان");

        RuleFor(p => p.Article.Text)
            .RequiredValidator("متن");

        RuleFor(p => p.Article.Summary)
            .RequiredValidator("توضیحات کوتاه")
            .MaxLengthValidator("توضیحات کوتاه", 250);

        RuleFor(p => p.Article.ImageFile)
            .MaxFileSizeValidator((3 * 1024 * 1024), false);
    }
}
