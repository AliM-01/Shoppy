using _01_Shoppy.Query.Contracts.Comment;
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
            .Where(x => x.OwnerRecordId == recordId && x.State == CM.Domain.Comment.CommentState.Confirmed)
             .OrderByDescending(x => x.LastUpdateDate)
              .Select(comment =>
                   _mapper.Map(comment, new CommentQueryModel()))
               .ToListAsync();

        return new Response<List<CommentQueryModel>>(comments);

    }

    #endregion
}
