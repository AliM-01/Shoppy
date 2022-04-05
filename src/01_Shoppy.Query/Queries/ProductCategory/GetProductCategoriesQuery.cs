﻿using _01_Shoppy.Query.Models.ProductCategory;

namespace _01_Shoppy.Query.Queries.ProductCategory;

public record GetProductCategoriesQuery() : IRequest<Response<IEnumerable<ProductCategoryQueryModel>>>;

public class GetProductCategoriesQueryHandler : IRequestHandler<GetProductCategoriesQuery, Response<IEnumerable<ProductCategoryQueryModel>>>
{
    #region Ctor

    private readonly IRepository<SM.Domain.ProductCategory.ProductCategory> _productCategoryRepository;
    private readonly IMapper _mapper;

    public GetProductCategoriesQueryHandler(IRepository<SM.Domain.ProductCategory.ProductCategory> productCategoryRepository, IMapper mapper)
    {
        _productCategoryRepository = Guard.Against.Null(productCategoryRepository, nameof(_productCategoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public Task<Response<IEnumerable<ProductCategoryQueryModel>>> Handle(GetProductCategoriesQuery request, CancellationToken cancellationToken)
    {
        var productCategories = _productCategoryRepository.AsQueryable()
                                                          .ToList()
                                                          .Select(c =>
                                                            _mapper.Map(c, new ProductCategoryQueryModel()))
                                                          .ToList();

        return Task.FromResult(new Response<IEnumerable<ProductCategoryQueryModel>>(productCategories));
    }
}
