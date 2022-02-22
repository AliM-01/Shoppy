using _0_Framework.Application.Models.Paging;
using DM.Application.Contracts.ColleagueDiscount.DTOs;
using DM.Application.Contracts.ColleagueDiscount.Queries;
using MongoDB.Driver.Linq;
using SM.Domain.Product;

namespace DM.Application.ColleagueDiscount.QueryHandles;
public class FilterColleagueDiscountsQueryHandler : IRequestHandler<FilterColleagueDiscountsQuery, Response<FilterColleagueDiscountDto>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ColleagueDiscount.ColleagueDiscount> _colleagueDiscountHelper;
    private readonly IGenericRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public FilterColleagueDiscountsQueryHandler(IGenericRepository<Domain.ColleagueDiscount.ColleagueDiscount> colleagueDiscountHelper,
        IGenericRepository<Product> productRepository, IMapper mapper)
    {
        _colleagueDiscountHelper = Guard.Against.Null(colleagueDiscountHelper, nameof(_colleagueDiscountHelper));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<FilterColleagueDiscountDto>> Handle(FilterColleagueDiscountsQuery request, CancellationToken cancellationToken)
    {
        var query = _colleagueDiscountHelper.AsQueryable();

        var products = await _productRepository.GetQuery().Select(x => new
        {
            x.Id,
            x.Title
        }).ToListAsync();

        #region filter

        if (request.Filter.ProductId != 0)
            query = query.Where(s => s.ProductId == request.Filter.ProductId);

        if (!string.IsNullOrEmpty(request.Filter.ProductTitle))
        {
            List<long> filteredProductIds = await _productRepository.GetQuery()
                .Where(s => EF.Functions.Like(s.Title, $"%{request.Filter.ProductTitle}%"))
                .Select(x => x.Id).ToListAsync();

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

        var pager = request.Filter.BuildPager(query.Count());

        var allEntities =
            _colleagueDiscountHelper
            .ApplyPagination(query, pager)
            .Select(discount =>
                _mapper.Map(discount, new ColleagueDiscountDto()))
            .ToList();

        allEntities.ForEach(discount =>
            discount.Product = products.FirstOrDefault(x => x.Id == discount.ProductId)?.Title);

        #endregion paging

        var returnData = request.Filter.SetData(allEntities).SetPaging(pager);

        if (returnData.Discounts is null)
            throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFoundMessage);

        if (returnData.PageId > returnData.GetLastPage() && returnData.GetLastPage() != 0)
            throw new NotFoundApiException();

        return new Response<FilterColleagueDiscountDto>(returnData);
    }
}