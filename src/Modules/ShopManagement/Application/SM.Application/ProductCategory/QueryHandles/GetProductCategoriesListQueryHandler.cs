using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SM.Application.Contracts.ProductCategory.DTOs;
using SM.Application.Contracts.ProductCategory.Queries;
using System.Collections.Generic;
using System.Linq;

namespace SM.Application.ProductCategory.QueryHandles;
public class GetProductCategoriesListQueryHandler : IRequestHandler<GetProductCategoriesListQuery, Response<IEnumerable<ProductCategoryDto>>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ProductCategory.ProductCategory> _productCategoryRepository;
    private readonly IMapper _mapper;

    public GetProductCategoriesListQueryHandler(IGenericRepository<Domain.ProductCategory.ProductCategory> productCategoryRepository, IMapper mapper)
    {
        _productCategoryRepository = Guard.Against.Null(productCategoryRepository, nameof(_productCategoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<IEnumerable<ProductCategoryDto>>> Handle(GetProductCategoriesListQuery request, CancellationToken cancellationToken)
    {
        var query = _productCategoryRepository.GetQuery()
            .OrderByDescending(p => p.LastUpdateDate).AsQueryable();

        #region paging

        var filteredEntities = await query
            .Select(product =>
                _mapper.Map(product, new ProductCategoryDto()))
            .ToListAsync(cancellationToken);

        #endregion paging

        if (filteredEntities is null)
            throw new ApiException(ApplicationErrorMessage.RecordNotFoundMessage);

        return new Response<IEnumerable<ProductCategoryDto>>(filteredEntities);
    }
}