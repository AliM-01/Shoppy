using _0_Framework.Application.ErrorMessages;
using _0_Framework.Application.Exceptions;
using _0_Framework.Application.Models.Paging;
using _01_Shoppy.Query.Helpers.Product;
using _01_Shoppy.Query.Models.ProductCategory;
using AutoMapper;
using SM.Infrastructure.Persistence.Context;

namespace _01_Shoppy.Query.Queries.ProductCategory;

public record GetProductCategoryWithProductsByQuery(FilterProductCategoryDetailsModel Filter) : IRequest<Response<ProductCategoryDetailsQueryModel>>;

public class GetProductCategoryWithProductsByQueryHandler : IRequestHandler<GetProductCategoryWithProductsByQuery, Response<ProductCategoryDetailsQueryModel>>
{
    #region Ctor

    private readonly ShopDbContext _context;
    private readonly IProductHelper _productHelper;
    private readonly IMapper _mapper;

    public GetProductCategoryWithProductsByQueryHandler(ShopDbContext context,
        IProductHelper productHelper, IMapper mapper)
    {
        _productHelper = Guard.Against.Null(productHelper, nameof(_productHelper));
        _context = Guard.Against.Null(context, nameof(_context));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<ProductCategoryDetailsQueryModel>> Handle(GetProductCategoryWithProductsByQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Filter.Slug))
            throw new NotFoundApiException();

        var categories = await _context.ProductCategories.Select(x => new
        {
            x.Slug,
            x.Id
        }).ToListAsync();

        string CategoryId = 0;

        #region filter

        var category = categories.FirstOrDefault(x => x.Slug == request.Filter.Slug);

        if (category is null)
            throw new NotFoundApiException();

        categoryId = category.Id;

        #endregion

        #region paging

        var productCategoryData = await _context.ProductCategories
                .Include(x => x.Products)
                .FirstOrDefaultAsync(x => x.Id == categoryId);

        var productsQuery = _context.Products
             .Where(x => x.CategoryId == productCategoryData.Id)
             .AsQueryable();

        var pager = Pager.Build(request.Filter.PageId, productsQuery.Count(),
            request.Filter.TakePage, request.Filter.ShownPages);

        var allEntities = await productsQuery.Paging(pager)
            .AsQueryable()
            .Select(p =>
               _productHelper.MapProductsFromProductCategories(p).Result)
            .ToListAsync(cancellationToken);

        #endregion

        var filteredData = request.Filter.SetData(allEntities).SetPaging(pager);

        if (filteredData.Products is null)
            throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFoundMessage);

        if (filteredData.PageId > filteredData.GetLastPage() && filteredData.GetLastPage() != 0)
            throw new NotFoundApiException();

        var returnData = new ProductCategoryDetailsQueryModel();

        returnData.ProductCategory = _mapper.Map(productCategoryData, new ProductCategoryQueryModel());
        returnData.FilterData = filteredData;

        return new Response<ProductCategoryDetailsQueryModel>(returnData);
    }
}
