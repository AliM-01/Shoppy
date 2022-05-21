using _01_Shoppy.Query.Helpers.Comment;
using _01_Shoppy.Query.Models.Comment;
using CM.Domain.Comment;

namespace _01_Shoppy.Query.Queries.Comment;

public record GetRecordCommentsByIdQuery(string RecordId) : IRequest<IEnumerable<CommentQueryModel>>;

public class GetRecordCommentsByIdQueryHandler : IRequestHandler<GetRecordCommentsByIdQuery, IEnumerable<CommentQueryModel>>
{
    #region Ctor

    private readonly IRepository<CM.Domain.Comment.Comment> _commentRepository;
    private readonly IMapper _mapper;

    public GetRecordCommentsByIdQueryHandler(
        IRepository<CM.Domain.Comment.Comment> commentRepository, IMapper mapper)
    {
        _commentRepository = Guard.Against.Null(commentRepository, nameof(_commentRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public Task<IEnumerable<CommentQueryModel>> Handle(GetRecordCommentsByIdQuery request, CancellationToken cancellationToken)
    {
        var comments = _commentRepository
            .AsQueryable(cancellationToken: cancellationToken)
            .Where(x => x.ParentId == null)
            .Where(x => x.OwnerRecordId == request.RecordId && x.State == CommentState.Confirmed)
            .ToList()
            .MapComments(_mapper);

        for (int i = 0; i < comments.Count; i++)
        {
            var replies = _commentRepository
                .AsQueryable()
                .Where(x => x.ParentId == comments[i].Id.ToString())
                .ToList()
                .MapComments(_mapper)
                .ToArray();

            comments[i].Replies = replies;
        }

        return Task.FromResult((IEnumerable<CommentQueryModel>)comments);
    }
}
