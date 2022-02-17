using CM.Domain.Comment;

namespace CM.Application.Comment.CommandHandles;

public class ConfirmCommentCommandHandler : IRequestHandler<ConfirmCommentCommand, Response<string>>
{
    #region Ctor

    private readonly ICommentDbContext _commentContext;
    private readonly IMapper _mapper;

    public ConfirmCommentCommandHandler(ICommentDbContext commentContext, IMapper mapper)
    {
        _commentContext = Guard.Against.Null(commentContext, nameof(_commentContext));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(ConfirmCommentCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Comment.Comment>.Filter.Eq(x => x.Id, request.CommentId);

        var comment = (await _commentContext.Comments.FindAsync(filter)).FirstOrDefault();

        if (comment is null)
            throw new NotFoundApiException();

        comment.State = CommentState.Confirmed;

        var res = await _commentContext.Comments.ReplaceOneAsync(filter, comment, new ReplaceOptions { IsUpsert = false });

        if (res.IsAcknowledged && res.ModifiedCount > 0)
            return new Response<string>("کامنت مورد نظر با موفقیت تایید شد");

        throw new ApiException(ApplicationErrorMessage.RecordNotFoundMessage);
    }
}