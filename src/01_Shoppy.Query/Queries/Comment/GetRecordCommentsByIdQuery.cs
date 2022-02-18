using _01_Shoppy.Query.Helpers.Comment;
using _01_Shoppy.Query.Models.Comment;
using AutoMapper;
using CM.Domain.Comment;
using CM.Infrastructure.Persistence.Context;
using MongoDB.Bson;
using MongoDB.Driver;

namespace _01_Shoppy.Query.Queries.Comment;

public record GetRecordCommentsByIdQuery
    (long RecordId) : IRequest<Response<List<CommentQueryModel>>>;

public class GetRecordCommentsByIdQueryHandler : IRequestHandler<GetRecordCommentsByIdQuery, Response<List<CommentQueryModel>>>
{
    #region Ctor

    private readonly ICommentDbContext _commentContext;
    private readonly IMapper _mapper;

    public GetRecordCommentsByIdQueryHandler(
        ICommentDbContext commentContext, IMapper mapper)
    {
        _commentContext = Guard.Against.Null(commentContext, nameof(_commentContext));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<List<CommentQueryModel>>> Handle(GetRecordCommentsByIdQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<CM.Domain.Comment.Comment>.Filter.Eq(x => x.ParentId, null)
             & Builders<CM.Domain.Comment.Comment>.Filter.Eq(x => x.OwnerRecordId, request.RecordId)
             & Builders<CM.Domain.Comment.Comment>.Filter.Eq(x => x.State, CommentState.Confirmed);

        var comments = (
            await _commentContext.Comments
                .FindAsync(filter)
            )
            .ToList()
            .MapComments(_mapper);

        for (int i = 0; i < comments.Count; i++)
        {
            var replies = (
                await _commentContext.Comments.FindAsync(new BsonDocument("parentId", comments[i].Id.ToString()))
                )
                .ToList()
                .MapComments(_mapper)
                .ToArray();

            comments[i].Replies = replies;
        }

        return new Response<List<CommentQueryModel>>(comments);
    }
}
