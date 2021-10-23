using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application.ErrorMessages;
using _0_Framework.Application.Exceptions;
using _0_Framework.Application.Models.Paging;
using _0_Framework.Application.Wrappers;
using _0_Framework.Domain.IGenericRepository;
using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SM.Application.Contracts.ProductCategory.DTOs;
using SM.Application.Contracts.ProductCategory.Queries;

namespace SM.Application.ProductCategory.QueryHandles
{
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

            var pager = Pager.Build(request.Filter.PageId, await query.CountAsync(cancellationToken), request.Filter.TakePage, request.Filter.ShownPages);
            var filteredEntities = await query.Paging(pager)
                .Select(product =>
                    _mapper.Map(product, new ProductCategoryDto()))
                .ToListAsync(cancellationToken);

            #endregion paging

            if (filteredEntities is null)
                throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFoundMessage);

            return new Response<FilterProductCategoryDto>(request.Filter.SetEntities(filteredEntities).SetPaging(pager));
        }
    }
}