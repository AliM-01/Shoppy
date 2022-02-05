using CM.Application.Contracts.Comment.Commands;

namespace BM.Infrastructure.Shared.Validators;

public class CancelCommentCommandValidator : AbstractValidator<CancelCommentCommand>
{
    public CancelCommentCommandValidator()
    {
        RuleFor(p => p.CommentId)
            .RangeValidator("شناسه کامنت", 1, 1000);
    }
}

