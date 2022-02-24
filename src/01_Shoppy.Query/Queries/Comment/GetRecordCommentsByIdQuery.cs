using _01_Shoppy.Query.Helpers.Comment;
using _01_Shoppy.Query.Models.Comment;
using AutoMapper;
using CM.Domain.Comment;

namespace _01_Shoppy.Query.Queries.Comment;

public record GetRecordCommentsByIdQuery
    (string RecordId) : IRequest<Response<List<CommentQueryModel>>>;

public class GetRecordCommentsByIdQueryHandler : IRequestHandler<GetRecordCommentsByIdQuery, Response<List<CommentQueryModel>>>
{
    #region Ctor

    private readonly IGenericRepository<CM.Domain.Comment.Comment> _commentRepository;
    private readonly IMapper _mapper;

    public GetRecordCommentsByIdQueryHandler(
        IGenericRepository<CM.Domain.Comment.Comment> commentRepository, IMapper mapper)
    {
        _commentRepository = Guard.Against.Null(commentRepository, nameof(_commentRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<List<CommentQueryModel>>> Handle(GetRecordCommentsByIdQuery request, CancellationToken cancellationToken)
    {
        var comments = (
            _commentRepository.AsQueryable()
            .Where(x => x.ParentId == null)
            .Where(x => x.OwnerRecordId == request.RecordId && x.State == CommentState.Confirmed)
            )
            .ToList()
            .MapComments(_mapper);

        for (int i = 0; i < comments.Count; i++)
        {
            var replies = (
                _commentRepository.AsQueryable().Where(x => x.ParentId == comments[i].Id.ToString())
                )
                .ToList()
                .MapComments(_mapper)
                .ToArray();

            comments[i].Replies = replies;
        }

        return new Response<List<CommentQueryModel>>(comments);
    }
}
