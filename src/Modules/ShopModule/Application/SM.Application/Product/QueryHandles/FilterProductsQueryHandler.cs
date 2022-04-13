﻿using _0_Framework.Application.Models.Paging;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;
using SM.Application.Contracts.Product.DTOs;
using SM.Application.Contracts.Product.Queries;
using System.Linq;

namespace SM.Application.Product.QueryHandles;
public class FilterProductsQueryHandler : IRequestHandler<FilterProductsQuery, ApiResult<FilterProductDto>>
{
    #region Ctor

    private readonly IRepository<Domain.Product.Product> _productRepository;
    private readonly IMapper _mapper;

    public FilterProductsQueryHandler(IRepository<Domain.Product.Product> productRepository, IMapper mapper)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<ApiResult<FilterProductDto>> Handle(FilterProductsQuery request, CancellationToken cancellationToken)
    {
        var query = _productRepository.AsQueryable();

        #region filter

        if (!string.IsNullOrEmpty(request.Filter.Search))
            query = query.Where(s => s.Title.Contains(request.Filter.Search) ||
             s.Code.Contains(request.Filter.Search));

        if (!string.IsNullOrEmpty(request.Filter.CategoryId))
            query = query.Where(s => s.CategoryId == request.Filter.CategoryId);

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
             _productRepository
             .ApplyPagination(query, pager)
             .Select(product =>
                _mapper.Map(product, new ProductDto()))
             .ToList();

        #endregion paging

        var returnData = request.Filter.SetData(allEntities).SetPaging(pager);

        if (returnData.Products is null)
            throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFound);

        if (returnData.PageId > returnData.GetLastPage() && returnData.GetLastPage() != 0)
            throw new NotFoundApiException();

        return ApiResponse.Success<FilterProductDto>(returnData);
    }
}