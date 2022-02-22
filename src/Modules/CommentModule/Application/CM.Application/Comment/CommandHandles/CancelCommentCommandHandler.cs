using CM.Domain.Comment;

namespace CM.Application.Comment.CommandHandles;

public class CancelCommentCommandHandler : IRequestHandler<CancelCommentCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Comment.Comment> _commentHelper;
    private readonly IMapper _mapper;

    public CancelCommentCommandHandler(IGenericRepository<Domain.Comment.Comment> commentHelper, IMapper mapper)
    {
        _commentHelper = Guard.Against.Null(commentHelper, nameof(_commentHelper));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(CancelCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentHelper.GetByIdAsync(request.CommentId);

        if (comment is null)
            throw new NotFoundApiException();

        comment.State = CommentState.Canceled;

        await _commentHelper.UpdateAsync(comment);

        return new Response<string>("کامنت مورد نظر با موفقیت رد شد");
    }
}