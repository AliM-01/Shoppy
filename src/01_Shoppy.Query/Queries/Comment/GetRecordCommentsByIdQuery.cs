using _0_Framework.Infrastructure.Helpers;
using _01_Shoppy.Query.Helpers.Comment;
using _01_Shoppy.Query.Models.Comment;
using AutoMapper;
using CM.Domain.Comment;

namespace _01_Shoppy.Query.Queries.Comment;

public record GetRecordCommentsByIdQuery
    (long RecordId) : IRequest<Response<List<CommentQueryModel>>>;

public class GetRecordCommentsByIdQueryHandler : IRequestHandler<GetRecordCommentsByIdQuery, Response<List<CommentQueryModel>>>
{
    #region Ctor

    private readonly IMongoHelper<CM.Domain.Comment.Comment> _commentHelper;
    private readonly IMapper _mapper;

    public GetRecordCommentsByIdQueryHandler(
        IMongoHelper<CM.Domain.Comment.Comment> commentHelper, IMapper mapper)
    {
        _commentHelper = Guard.Against.Null(commentHelper, nameof(_commentHelper));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<List<CommentQueryModel>>> Handle(GetRecordCommentsByIdQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<CM.Domain.Comment.Comment>.Filter.Eq(x => x.ParentId, null)
             & Builders<CM.Domain.Comment.Comment>.Filter.Eq(x => x.OwnerRecordId, request.RecordId)
             & Builders<CM.Domain.Comment.Comment>.Filter.Eq(x => x.State, CommentState.Confirmed);

        var comments = (
            _commentHelper.GetQuery()
            .Where(x => x.ParentId == null)
            .Where(x => x.OwnerRecordId == request.RecordId && x.State == CommentState.Confirmed)
            )
            .ToList()
            .MapComments(_mapper);

        for (int i = 0; i < comments.Count; i++)
        {
            var replies = (
                _commentHelper.GetQuery().Where(x => x.ParentId == comments[i].Id.ToString())
                )
                .ToList()
                .MapComments(_mapper)
                .ToArray();

            comments[i].Replies = replies;
        }

        return new Response<List<CommentQueryModel>>(comments);
    }
}
