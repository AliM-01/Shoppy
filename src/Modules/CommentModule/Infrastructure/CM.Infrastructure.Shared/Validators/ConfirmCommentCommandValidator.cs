using CM.Application.Contracts.Comment.Commands;

namespace BM.Infrastructure.Shared.Validators;

public class ConfirmCommentCommandValidator : AbstractValidator<ConfirmCommentCommand>
{
    public ConfirmCommentCommandValidator()
    {
        RuleFor(p => p.CommentId)
            .RangeValidator("شناسه کامنت", 1, 1000);
    }
}

