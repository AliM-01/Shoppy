using _01_Shoppy.Query.Helpers.Comment;
using _01_Shoppy.Query.Models.Comment;
using AutoMapper;
using CM.Infrastructure.Persistence.Context;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

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
        var comments = await _commentContext.Comments
            .AsQueryable()
            .Where(x => x.ParentId == null)
            .Where(x => x.OwnerRecordId == request.RecordId && x.State == CM.Domain.Comment.CommentState.Confirmed)
            .MapComments(_mapper)
            .ToListAsync();

        for (int i = 0; i < comments.Count; i++)
        {
            var replies = await _commentContext.Comments
                .AsQueryable()
                .Where(x => x.ParentId == comments[i].Id.ToString())
                .MapComments(_mapper)
                .ToArrayAsync();

            comments[i].Replies = replies;
        }

        return new Response<List<CommentQueryModel>>(comments);
    }
}
