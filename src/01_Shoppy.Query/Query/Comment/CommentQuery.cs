using _01_Shoppy.Query.Contracts.Comment;
using _01_Shoppy.Query.Helpers.Comment;
using AutoMapper;
using CM.Infrastructure.Persistence.Context;

namespace _01_Shoppy.Query.Query.Comment;

public class CommentQuery : ICommentQuery
{
    #region Ctor

    private readonly CommentDbContext _commentContext;
    private readonly IMapper _mapper;

    public CommentQuery(
        CommentDbContext commentContext, IMapper mapper)
    {
        _commentContext = Guard.Against.Null(commentContext, nameof(_commentContext));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    #region Get Record Comments By Id

    public async Task<Response<List<CommentQueryModel>>> GetRecordCommentsById(long recordId)
    {
        var comments = await _commentContext.Comments
            .Where(x => x.ParentId == 0 || x.ParentId == null)
            .Where(x => x.OwnerRecordId == recordId && x.State == CM.Domain.Comment.CommentState.Confirmed)
            .MapComments(_mapper)
            .ToListAsync();

        for (int i = 0; i < comments.Count; i++)
        {
            var replies = await _commentContext.Comments
                .Where(x => x.ParentId == comments[i].Id)
                .MapComments(_mapper)
                .ToArrayAsync();

            comments[i].Replies = replies;
        }

        return new Response<List<CommentQueryModel>>(comments);

    }

    #endregion
}
