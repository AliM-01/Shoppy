using CM.Domain.Comment;

namespace CM.Application.Comment.CommandHandles;

public class ConfirmCommentCommandHandler : IRequestHandler<ConfirmCommentCommand, Response<string>>
{
    #region Ctor

    private readonly IMongoHelper<Domain.Comment.Comment> _commentHelper;
    private readonly IMapper _mapper;

    public ConfirmCommentCommandHandler(IMongoHelper<Domain.Comment.Comment> commentHelper, IMapper mapper)
    {
        _commentHelper = Guard.Against.Null(commentHelper, nameof(_commentHelper));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(ConfirmCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentHelper.GetByIdAsync(request.CommentId);

        if (comment is null)
            throw new NotFoundApiException();

        comment.State = CommentState.Canceled;

        await _commentHelper.UpdateAsync(comment);

        return new Response<string>("کامنت مورد نظر با موفقیت تایید شد");
    }
}