using CM.Domain.Comment;
using FluentValidation;

namespace CM.Application.Comment.Commands;

public record CancelCommentCommand(string CommentId) : IRequest<ApiResult>;

public class CancelCommentCommandValidator : AbstractValidator<CancelCommentCommand>
{
    public CancelCommentCommandValidator()
    {
        RuleFor(p => p.CommentId)
            .RequiredValidator("شناسه کامنت");
    }
}

public class CancelCommentCommandHandler : IRequestHandler<CancelCommentCommand, ApiResult>
{
    private readonly IRepository<Domain.Comment.Comment> _commentRepository;
    private readonly IMapper _mapper;

    public CancelCommentCommandHandler(IRepository<Domain.Comment.Comment> commentRepository, IMapper mapper)
    {
        _commentRepository = Guard.Against.Null(commentRepository, nameof(_commentRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<ApiResult> Handle(CancelCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.FindByIdAsync(request.CommentId);

        NotFoundApiException.ThrowIfNull(comment);

        comment.State = CommentState.Canceled;

        await _commentRepository.UpdateAsync(comment);

        return ApiResponse.Success("کامنت مورد نظر با موفقیت رد شد");
    }
}