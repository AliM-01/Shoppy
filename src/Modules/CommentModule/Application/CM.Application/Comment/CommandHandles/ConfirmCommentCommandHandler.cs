using CM.Domain.Comment;

namespace CM.Application.Comment.CommandHandles;

public class ConfirmCommentCommandHandler : IRequestHandler<ConfirmCommentCommand, Response<string>>
{
    #region Ctor

    private readonly IRepository<Domain.Comment.Comment> _commentRepository;
    private readonly IMapper _mapper;

    public ConfirmCommentCommandHandler(IRepository<Domain.Comment.Comment> commentRepository, IMapper mapper)
    {
        _commentRepository = Guard.Against.Null(commentRepository, nameof(_commentRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(ConfirmCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByIdAsync(request.CommentId);

        if (comment is null)
            throw new NotFoundApiException();

        comment.State = CommentState.Canceled;

        await _commentRepository.UpdateAsync(comment);

        return new Response<string>("کامنت مورد نظر با موفقیت تایید شد");
    }
}