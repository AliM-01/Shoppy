using _0_Framework.Application.Models.Paging;
using DM.Application.Contracts.CustomerDiscount.DTOs;
using DM.Application.Contracts.CustomerDiscount.Queries;
using SM.Domain.Product;

namespace DM.Application.CustomerDiscount.QueryHandles;
public class FilterCustomerDiscountsQueryHandler : IRequestHandler<FilterCustomerDiscountsQuery, Response<FilterCustomerDiscountDto>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.CustomerDiscount.CustomerDiscount> _customerDiscountRepository;
    private readonly IGenericRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public FilterCustomerDiscountsQueryHandler(IGenericRepository<Domain.CustomerDiscount.CustomerDiscount> customerDiscountRepository,
        IGenericRepository<Product> productRepository, IMapper mapper)
    {
        _customerDiscountRepository = Guard.Against.Null(customerDiscountRepository, nameof(_customerDiscountRepository));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<FilterCustomerDiscountDto>> Handle(FilterCustomerDiscountsQuery request, CancellationToken cancellationToken)
    {
        var query = _customerDiscountRepository.GetQuery()
            .OrderByDescending(p => p.LastUpdateDate).AsQueryable();

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

        #endregion filter

        #region paging

        var pager = Pager.Build(request.Filter.PageId, await query.CountAsync(cancellationToken),
            request.Filter.TakePage, request.Filter.ShownPages);
        var allEntities = await query.Paging(pager)
            .OrderByDescending(x => x.CreationDate)
            .Select(discount =>
                _mapper.Map(discount, new CustomerDiscountDto()))
            .ToListAsync(cancellationToken);

        allEntities.ForEach(discount =>
            discount.Product = products.FirstOrDefault(x => x.Id == discount.ProductId)?.Title);

        #endregion paging

        var returnData = request.Filter.SetData(allEntities).SetPaging(pager);

        if (returnData.Discounts is null)
            throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFoundMessage);

        if (returnData.PageId > returnData.GetLastPage() && returnData.GetLastPage() != 0)
            throw new NotFoundApiException();

        return new Response<FilterCustomerDiscountDto>(returnData);
    }
}