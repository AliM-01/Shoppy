using CM.Domain.Comment;
using FluentValidation;

namespace CM.Application.Comment.Commands;

public record ConfirmCommentCommand(string CommentId) : IRequest<ApiResult>;

public class ConfirmCommentCommandValidator : AbstractValidator<ConfirmCommentCommand>
{
    public ConfirmCommentCommandValidator()
    {
        RuleFor(p => p.CommentId)
            .RequiredValidator("شناسه کامنت");
    }
}

public class ConfirmCommentCommandHandler : IRequestHandler<ConfirmCommentCommand, ApiResult>
{
    private readonly IRepository<Domain.Comment.Comment> _commentRepository;
    private readonly IMapper _mapper;

    public ConfirmCommentCommandHandler(IRepository<Domain.Comment.Comment> commentRepository, IMapper mapper)
    {
        _commentRepository = Guard.Against.Null(commentRepository, nameof(_commentRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<ApiResult> Handle(ConfirmCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.FindByIdAsync(request.CommentId);

        NotFoundApiException.ThrowIfNull(comment);

        comment.State = CommentState.Confirmed;

        await _commentRepository.UpdateAsync(comment);

        return ApiResponse.Success("کامنت مورد نظر با موفقیت تایید شد");
    }
}