using DM.Application.Contracts.ColleagueDiscount.DTOs;
using DM.Application.Contracts.ColleagueDiscount.Queries;
using SM.Domain.Product;

namespace DM.Application.ColleagueDiscount.QueryHandles;
public class FilterColleagueDiscountsQueryHandler : IRequestHandler<FilterColleagueDiscountsQuery, Response<FilterColleagueDiscountDto>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ColleagueDiscount.ColleagueDiscount> _colleagueDiscountRepository;
    private readonly IGenericRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public FilterColleagueDiscountsQueryHandler(IGenericRepository<Domain.ColleagueDiscount.ColleagueDiscount> colleagueDiscountRepository,
        IGenericRepository<Product> productRepository, IMapper mapper)
    {
        _colleagueDiscountRepository = Guard.Against.Null(colleagueDiscountRepository, nameof(_colleagueDiscountRepository));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<FilterColleagueDiscountDto>> Handle(FilterColleagueDiscountsQuery request, CancellationToken cancellationToken)
    {
        var query = _colleagueDiscountRepository.GetQuery()
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

        var filteredEntities = await query
            .Select(discount =>
                _mapper.Map(discount, new ColleagueDiscountDto()))
            .ToListAsync(cancellationToken);

        filteredEntities.ForEach(discount =>
            discount.Product = products.FirstOrDefault(x => x.Id == discount.ProductId)?.Title);

        #endregion paging

        if (filteredEntities is null)
            throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFoundMessage);

        request.Filter.Discounts = filteredEntities;

        return new Response<FilterColleagueDiscountDto>(request.Filter);
    }
}