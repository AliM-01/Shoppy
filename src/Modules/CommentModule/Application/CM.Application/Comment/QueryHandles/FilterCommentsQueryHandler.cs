using _0_Framework.Application.Models.Paging;
using CM.Application.Contracts.Comment.DTOs;
using CM.Application.Contracts.Inventory.Queries;
using CM.Domain.Comment;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CM.Application.Comment.QueryHandles;

public class FilterCommentsQueryHandler : IRequestHandler<FilterCommentsQuery, Response<FilterCommentDto>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Comment.Comment> _commentRepository;
    private readonly IMapper _mapper;

    public FilterCommentsQueryHandler(IGenericRepository<Domain.Comment.Comment> commentRepository, IMapper mapper)
    {
        _commentRepository = Guard.Against.Null(commentRepository, nameof(_commentRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<FilterCommentDto>> Handle(FilterCommentsQuery request, CancellationToken cancellationToken)
    {
        var query = _commentRepository
            .GetQuery()
            .IgnoreQueryFilters()
            .AsQueryable();

        #region filter

        switch (request.Filter.State)
        {
            case FilterCommentState.All:
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

        var pager = Pager.Build(request.Filter.PageId, query.Count(),
            request.Filter.TakePage, request.Filter.ShownPages);
        var allEntities = await query.Paging(pager)
            .AsQueryable()
            .Select(inventory =>
                _mapper.Map(inventory, new CommentDto()))
            .ToListAsync();

        #endregion paging

        var returnData = request.Filter.SetData(allEntities).SetPaging(pager);

        if (returnData.Comments is null)
            throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFoundMessage);

        if (returnData.PageId > returnData.GetLastPage() && returnData.GetLastPage() != 0)
            throw new NotFoundApiException();

        return new Response<FilterCommentDto>(returnData);
    }
}
