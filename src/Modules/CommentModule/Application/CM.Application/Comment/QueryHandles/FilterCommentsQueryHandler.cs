using _0_Framework.Application.Models.Paging;
using CM.Application.Contracts.Comment.DTOs;
using CM.Application.Contracts.Inventory.Queries;
using CM.Application.Contracts.Sevices;
using CM.Domain.Comment;
using MongoDB.Driver.Linq;
using System.Linq;

namespace CM.Application.Comment.QueryHandles;

public class FilterCommentsQueryHandler : IRequestHandler<FilterCommentsQuery, ApiResult<FilterCommentDto>>
{
    #region Ctor

    private readonly IRepository<CM.Domain.Comment.Comment> _commentRepository;
    private readonly IMapper _mapper;
    private readonly ICMProductAcl _productAcl;
    private readonly ICMArticleAcl _articleAcl;

    public FilterCommentsQueryHandler(IRepository<CM.Domain.Comment.Comment> commentRepository,
                                      IMapper mapper,
                                      ICMProductAcl productAcl,
                                      ICMArticleAcl articleAcl)
    {
        _commentRepository = Guard.Against.Null(commentRepository, nameof(_commentRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _productAcl = Guard.Against.Null(productAcl, nameof(_productAcl));
        _articleAcl = Guard.Against.Null(articleAcl, nameof(_articleAcl));
    }

    #endregion

    public async Task<ApiResult<FilterCommentDto>> Handle(FilterCommentsQuery request, CancellationToken cancellationToken)
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

        foreach (var item in allEntities)
        {
            switch (item.Type)
            {
                case CommentType.Product:
                    item.OwnerName = await _productAcl.GetProductTitle(item.OwnerRecordId);
                    break;

                case CommentType.Article:
                    item.OwnerName = await _articleAcl.GetArticleTitle(item.OwnerRecordId);
                    break;

                default:
                    break;
            }
        }

        #endregion paging

        var returnData = request.Filter.SetData(allEntities).SetPaging(pager);

        if (returnData.Comments is null)
            throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFound);

        if (returnData.PageId > returnData.GetLastPage() && returnData.GetLastPage() != 0)
            throw new NotFoundApiException();

        return ApiResponse.Success<FilterCommentDto>(returnData);
    }
}
