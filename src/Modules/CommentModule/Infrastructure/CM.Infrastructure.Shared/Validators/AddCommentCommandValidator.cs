using CM.Application.Contracts.Comment.Commands;

namespace CM.Infrastructure.Shared.Validators;

public class AddCommentCommandValidator : AbstractValidator<AddCommentCommand>
{
    public AddCommentCommandValidator()
    {
        RuleFor(p => p.Comment.Name)
            .RequiredValidator("نام");

        RuleFor(p => p.Comment.Email)
            .CustomEmailAddressValidator();

        RuleFor(p => p.Comment.Text)
            .RequiredValidator("نام")
            .MaxLengthValidator("متن نظر", 500);

        RuleFor(p => p.Comment.OwnerRecordId)
            .RequiredValidator("شناسه محصول/مقاله");

        RuleFor(p => p.Comment.ParentId)
            .RequiredValidator("شناسه والد");
    }
}