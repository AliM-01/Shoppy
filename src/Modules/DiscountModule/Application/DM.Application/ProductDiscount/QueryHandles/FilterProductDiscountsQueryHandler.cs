using _0_Framework.Application.Models.Paging;
using _0_Framework.Infrastructure;
using DM.Application.Contracts.ProductDiscount.DTOs;
using DM.Application.Contracts.ProductDiscount.Queries;
using MongoDB.Driver.Linq;
using SM.Domain.Product;

namespace DM.Application.ProductDiscount.QueryHandles;
public class FilterProductDiscountsQueryHandler : IRequestHandler<FilterProductDiscountsQuery, Response<FilterProductDiscountDto>>
{
    #region Ctor

    private readonly IRepository<Domain.ProductDiscount.ProductDiscount> _productDiscountRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public FilterProductDiscountsQueryHandler(IRepository<Domain.ProductDiscount.ProductDiscount> productDiscountRepository,
        IRepository<Product> productRepository, IMapper mapper)
    {
        _productDiscountRepository = Guard.Against.Null(productDiscountRepository, nameof(_productDiscountRepository));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<FilterProductDiscountDto>> Handle(FilterProductDiscountsQuery request, CancellationToken cancellationToken)
    {
        var query = _productDiscountRepository.AsQueryable();

        var products = await _productRepository.AsQueryable()
            .Select(x => new
            {
                x.Id,
                x.Title
            }).ToListAsyncSafe();

        #region filter

        if (!string.IsNullOrEmpty(request.Filter.ProductId))
            query = query.Where(s => s.ProductId == request.Filter.ProductId);

        if (!string.IsNullOrEmpty(request.Filter.ProductTitle))
        {
            List<string> filteredProductIds = await _productRepository.AsQueryable()
                .Where(s => EF.Functions.Like(s.Title, $"%{request.Filter.ProductTitle}%"))
                .Select(x => x.Id).ToListAsyncSafe();

            query = query.Where(s => filteredProductIds.Contains(s.ProductId));
        }

        switch (request.Filter.SortDateOrder)
        {
            case PagingDataSortCreationDateOrder.DES:
                query = query.OrderByDescending(x => x.CreationDate);
                break;

            case PagingDataSortCreationDateOrder.ASC:
                query = query.OrderBy(x => x.CreationDate);
                break;
        }

        switch (request.Filter.SortIdOrder)
        {
            case PagingDataSortIdOrder.NotSelected:
                break;

            case PagingDataSortIdOrder.DES:
                query = query.OrderByDescending(x => x.Id);
                break;

            case PagingDataSortIdOrder.ASC:
                query = query.OrderBy(x => x.Id);
                break;
        }

        #endregion filter

        #region paging

        var pager = request.Filter.BuildPager((await query.CountAsync()));

        var allEntities =
            _productDiscountRepository
            .ApplyPagination(query, pager)
            .Select(discount =>
                _mapper.Map(discount, new ProductDiscountDto()))
            .ToList();

        allEntities.ForEach(discount =>
            discount.Product = products.FirstOrDefault(x => x.Id == discount.ProductId)?.Title);

        #endregion paging

        var returnData = request.Filter.SetData(allEntities).SetPaging(pager);

        if (returnData.Discounts is null)
            throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFoundMessage);

        if (returnData.PageId > returnData.GetLastPage() && returnData.GetLastPage() != 0)
            throw new NotFoundApiException();

        return new Response<FilterProductDiscountDto>(returnData);
    }
}