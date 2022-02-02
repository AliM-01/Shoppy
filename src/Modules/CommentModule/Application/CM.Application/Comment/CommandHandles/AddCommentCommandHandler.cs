namespace CM.Application.Comment.CommandHandles;

public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Comment.Comment> _commentRepository;
    private readonly IMapper _mapper;

    public AddCommentCommandHandler(IGenericRepository<Domain.Comment.Comment> commentRepository, IMapper mapper)
    {
        _commentRepository = Guard.Against.Null(commentRepository, nameof(_commentRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = _mapper.Map(request.Comment, new Domain.Comment.Comment());

        if (comment.ParentId == 0)
            comment.ParentId = null;

        await _commentRepository.InsertEntity(comment);
        await _commentRepository.SaveChanges();

        return new Response<string>("کامنت با موفقیت ثبت شد و پس از تایید توسط ادمین در سایت نمایش داده خواهد شد");
    }
}

