﻿using _0_Framework.Infrastructure;
using _01_Shoppy.Query.Helpers.Comment;
using _01_Shoppy.Query.Models.Comment;
using CM.Domain.Comment;

namespace _01_Shoppy.Query.Queries.Comment;

public record GetRecordCommentsByIdQuery
    (string RecordId) : IRequest<ApiResult<List<CommentQueryModel>>>;

public class GetRecordCommentsByIdQueryHandler : IRequestHandler<GetRecordCommentsByIdQuery, ApiResult<List<CommentQueryModel>>>
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

    public async Task<ApiResult<List<CommentQueryModel>>> Handle(GetRecordCommentsByIdQuery request, CancellationToken cancellationToken)
    {
        var comments = (await
            _commentRepository.AsQueryable(cancellationToken: cancellationToken)
            .Where(x => x.ParentId == null)
            .Where(x => x.OwnerRecordId == request.RecordId && x.State == CommentState.Confirmed)
            .ToListAsyncSafe()
            )
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

        return ApiResponse.Success<List<CommentQueryModel>>(comments);
    }
}
