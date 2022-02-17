
namespace CM.Application.Comment.CommandHandles;

public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, Response<string>>
{
    #region Ctor

    private readonly ICommentDbContext _commentContext;
    private readonly IMapper _mapper;

    public AddCommentCommandHandler(ICommentDbContext commentContext, IMapper mapper)
    {
        _commentContext = Guard.Against.Null(commentContext, nameof(_commentContext));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = _mapper.Map(request.Comment, new Domain.Comment.Comment());

        if (string.IsNullOrEmpty(comment.ParentId))
            comment.ParentId = null;

        await _commentContext.Comments.InsertOneAsync(comment);

        return new Response<string>("کامنت با موفقیت ثبت شد و پس از تایید توسط ادمین در سایت نمایش داده خواهد شد");
    }
}

