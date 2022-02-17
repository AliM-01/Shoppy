using CM.Application.Contracts.Comment.Commands;

namespace BM.Infrastructure.Shared.Validators;

public class CancelCommentCommandValidator : AbstractValidator<CancelCommentCommand>
{
    public CancelCommentCommandValidator()
    {
        RuleFor(p => p.CommentId)
            .RequiredValidator("شناسه کامنت");
    }
}

