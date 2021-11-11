using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SM.Application.Contracts.ProductCategory.DTOs;
using SM.Application.Contracts.ProductCategory.Queries;
using System.Linq;

namespace SM.Application.ProductCategory.QueryHandles;
public class FilterProductCategoriesQueryHandler : IRequestHandler<FilterProductCategoriesQuery, Response<FilterProductCategoryDto>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ProductCategory.ProductCategory> _productCategoryRepository;
    private readonly IMapper _mapper;

    public FilterProductCategoriesQueryHandler(IGenericRepository<Domain.ProductCategory.ProductCategory> productCategoryRepository, IMapper mapper)
    {
        _productCategoryRepository = Guard.Against.Null(productCategoryRepository, nameof(_productCategoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<FilterProductCategoryDto>> Handle(FilterProductCategoriesQuery request, CancellationToken cancellationToken)
    {
        var query = _productCategoryRepository.GetQuery()
            .OrderByDescending(p => p.LastUpdateDate).AsQueryable();

        #region filter

        if (!string.IsNullOrEmpty(request.Filter.Title))
            query = query.Where(s => EF.Functions.Like(s.Title, $"%{request.Filter.Title}%"));

        #endregion filter

        #region paging

        var filteredEntities = await query
            .Select(product =>
                _mapper.Map(product, new ProductCategoryDto()))
            .ToListAsync(cancellationToken);

        #endregion paging

        if (filteredEntities is null)
            throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFoundMessage);

        request.Filter.ProductCategories = filteredEntities;

        return new Response<FilterProductCategoryDto>(request.Filter);
    }
}