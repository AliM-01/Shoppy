using CM.Domain.Comment;

namespace CM.Application.Comment.CommandHandles;

public class CancelCommentCommandHandler : IRequestHandler<CancelCommentCommand, ApiResult>
{
    #region Ctor

    private readonly IRepository<Domain.Comment.Comment> _commentRepository;
    private readonly IMapper _mapper;

    public CancelCommentCommandHandler(IRepository<Domain.Comment.Comment> commentRepository, IMapper mapper)
    {
        _commentRepository = Guard.Against.Null(commentRepository, nameof(_commentRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<ApiResult> Handle(CancelCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.FindByIdAsync(request.CommentId);

        NotFoundApiException.ThrowIfNull(comment);

        comment.State = CommentState.Canceled;

        await _commentRepository.UpdateAsync(comment);

        return ApiResponse.Success("کامنت مورد نظر با موفقیت رد شد");
    }
}