using AutoMapper;
using DM.Application.Contracts.CustomerDiscount.DTOs;
using DM.Application.Contracts.CustomerDiscount.Queries;
using Microsoft.EntityFrameworkCore;
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

        #endregion filter

        #region paging

        var filteredEntities = await query
            .Select(discount =>
                _mapper.Map(discount, new CustomerDiscountDto
                {
                    Product = products.FirstOrDefault(x => x.Id == discount.ProductId).Title
                }))
            .ToListAsync(cancellationToken);

        #endregion paging

        if (filteredEntities is null)
            throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFoundMessage);

        request.Filter.Discounts = filteredEntities;

        return new Response<FilterCustomerDiscountDto>(request.Filter);
    }
}