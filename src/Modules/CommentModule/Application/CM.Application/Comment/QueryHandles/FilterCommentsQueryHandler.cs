using _0_Framework.Application.Models.Paging;
using CM.Application.Contracts.Comment.DTOs;
using CM.Application.Contracts.Inventory.Queries;
using CM.Domain.Comment;
using MongoDB.Driver.Linq;
using System.Linq;

namespace CM.Application.Comment.QueryHandles;

public class FilterCommentsQueryHandler : IRequestHandler<FilterCommentsQuery, Response<FilterCommentDto>>
{
    #region Ctor

    private readonly IGenericRepository<CM.Domain.Comment.Comment> _commentRepository;
    private readonly IMapper _mapper;

    public FilterCommentsQueryHandler(IGenericRepository<CM.Domain.Comment.Comment> commentRepository, IMapper mapper)
    {
        _commentRepository = Guard.Against.Null(commentRepository, nameof(_commentRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<FilterCommentDto>> Handle(FilterCommentsQuery request, CancellationToken cancellationToken)
    {
        var query = _commentRepository.AsQueryable(cancellationToken: cancellationToken);

        #region filter

        switch (request.Filter.State)
        {
            case FilterCommentState.All:
                break;

            case FilterCommentState.UnderProgress:
                query = query.Where(s => s.State == CommentState.UnderProgress);
                break;

            case FilterCommentState.Confirmed:
                query = query.Where(s => s.State == CommentState.Confirmed);
                break;

            case FilterCommentState.Canceled:
                query = query.Where(s => s.State == CommentState.Canceled);
                break;
        }

        #endregion filter

        #region paging

        var pager = request.Filter.BuildPager((await query.CountAsync()), cancellationToken);

        var allEntities =
             _commentRepository
             .ApplyPagination(query, pager, cancellationToken)
             .Select(c => _mapper.Map(c, new CommentDto()))
             .ToList();

        #endregion paging

        var returnData = request.Filter.SetData(allEntities).SetPaging(pager);

        if (returnData.Comments is null)
            throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFoundMessage);

        if (returnData.PageId > returnData.GetLastPage() && returnData.GetLastPage() != 0)
            throw new NotFoundApiException();

        return new Response<FilterCommentDto>(returnData);
    }
}
