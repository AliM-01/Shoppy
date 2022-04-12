

namespace CM.Application.Comment.CommandHandles;

public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, ApiResult>
{
    #region Ctor

    private readonly IRepository<Domain.Comment.Comment> _commentRepository;
    private readonly IMapper _mapper;

    public AddCommentCommandHandler(IRepository<Domain.Comment.Comment> commentRepository, IMapper mapper)
    {
        _commentRepository = Guard.Against.Null(commentRepository, nameof(_commentRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<ApiResult> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = _mapper.Map(request.Comment, new Domain.Comment.Comment());

        if (string.IsNullOrEmpty(comment.ParentId))
            comment.ParentId = null;

        await _commentRepository.InsertAsync(comment);

        return ApiResponse.Success("کامنت با موفقیت ثبت شد و پس از تایید توسط ادمین در سایت نمایش داده خواهد شد");
    }
}

