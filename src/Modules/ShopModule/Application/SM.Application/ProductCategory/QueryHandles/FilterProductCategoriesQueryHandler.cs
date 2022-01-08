using _0_Framework.Application.Models.Paging;
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
            .Include(p => p.Products).AsQueryable();

        #region filter

        if (!string.IsNullOrEmpty(request.Filter.Title))
            query = query.Where(s => EF.Functions.Like(s.Title, $"%{request.Filter.Title}%"));

        switch (request.Filter.SortDateOrder)
        {
            case PagingDataSortCreationDateOrder.DES:
                query = query.OrderByDescending(x => x.CreationDate).AsQueryable();
                break;

            case PagingDataSortCreationDateOrder.ASC:
                query = query.OrderBy(x => x.CreationDate).AsQueryable();
                break;
        }

        switch (request.Filter.SortIdOrder)
        {
            case PagingDataSortIdOrder.NotSelected:
                break;

            case PagingDataSortIdOrder.DES:
                query = query.OrderByDescending(x => x.Id).AsQueryable();
                break;

            case PagingDataSortIdOrder.ASC:
                query = query.OrderBy(x => x.Id).AsQueryable();
                break;
        }

        #endregion filter

        #region paging

        var pager = Pager.Build(request.Filter.PageId, await query.CountAsync(cancellationToken),
            request.Filter.TakePage, request.Filter.ShownPages);
        var allEntities = await query.Paging(pager)
            .AsQueryable()
            .Select(product =>
                _mapper.Map(product, new ProductCategoryDto
                {
                    ProductsCount = product.Products.Count()
                }))
            .ToListAsync(cancellationToken);

        #endregion paging

        var returnData = request.Filter.SetData(allEntities).SetPaging(pager);

        if (returnData.ProductCategories is null)
            throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFoundMessage);

        if (returnData.PageId > returnData.GetLastPage() && returnData.GetLastPage() != 0)
            throw new NotFoundApiException();

        return new Response<FilterProductCategoryDto>(returnData);
    }
}