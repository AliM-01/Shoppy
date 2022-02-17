using CM.Application.Contracts.Comment.Commands;

namespace BM.Infrastructure.Shared.Validators;

public class ConfirmCommentCommandValidator : AbstractValidator<ConfirmCommentCommand>
{
    public ConfirmCommentCommandValidator()
    {
        RuleFor(p => p.CommentId)
            .RequiredValidator("شناسه کامنت");
    }
}

