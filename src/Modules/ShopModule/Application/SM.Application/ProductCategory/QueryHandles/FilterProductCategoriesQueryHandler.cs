using _0_Framework.Application.Models.Paging;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;
using SM.Application.Contracts.ProductCategory.DTOs;
using SM.Application.Contracts.ProductCategory.Queries;
using System.Linq;

namespace SM.Application.ProductCategory.QueryHandles;
public class FilterProductCategoriesQueryHandler : IRequestHandler<FilterProductCategoriesQuery, Response<FilterProductCategoryDto>>
{
    #region Ctor

    private readonly IRepository<Domain.ProductCategory.ProductCategory> _productCategoryRepository;
    private readonly IRepository<Domain.Product.Product> _productRepository;
    private readonly IMapper _mapper;

    public FilterProductCategoriesQueryHandler(IRepository<Domain.ProductCategory.ProductCategory> productCategoryRepository,
        IMapper mapper, IRepository<Domain.Product.Product> productRepository)
    {
        _productCategoryRepository = Guard.Against.Null(productCategoryRepository, nameof(_productCategoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));

    }

    #endregion

    public async Task<Response<FilterProductCategoryDto>> Handle(FilterProductCategoriesQuery request, CancellationToken cancellationToken)
    {
        var query = _productCategoryRepository.AsQueryable();

        #region filter

        if (!string.IsNullOrEmpty(request.Filter.Title))
            query = query.Where(s => EF.Functions.Like(s.Title, $"%{request.Filter.Title}%"));

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
             _productCategoryRepository
                .ApplyPagination(query, pager)
                .Select(category =>
                    _mapper.Map(category, new ProductCategoryDto
                    {
                        ProductsCount = _productRepository.AsQueryable().Where(x => x.CategoryId == category.Id).Count()
                    }))
                .ToList();

        #endregion paging

        var returnData = request.Filter.SetData(allEntities).SetPaging(pager);

        if (returnData.ProductCategories is null)
            throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFoundMessage);

        if (returnData.PageId > returnData.GetLastPage() && returnData.GetLastPage() != 0)
            throw new NotFoundApiException();

        return new Response<FilterProductCategoryDto>(returnData);
    }
}