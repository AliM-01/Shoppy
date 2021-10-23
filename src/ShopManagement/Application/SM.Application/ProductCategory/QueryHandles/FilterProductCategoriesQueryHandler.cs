using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application.ErrorMessages;
using _0_Framework.Application.Exceptions;
using _0_Framework.Application.Wrappers;
using _0_Framework.Domain.IGenericRepository;
using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SM.Application.Contracts.ProductCategory.Models;
using SM.Application.Contracts.ProductCategory.Queries;

namespace SM.Application.ProductCategory.QueryHandles
{
    public class FilterProductCategoriesQueryHandler : IRequestHandler<FilterProductCategoriesQuery, Response<IEnumerable<ProductCategoryDto>>>
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

        public async Task<Response<IEnumerable<ProductCategoryDto>>> Handle(FilterProductCategoriesQuery request, CancellationToken cancellationToken)
        {
            var query = _productCategoryRepository.GetQuery()
                .OrderByDescending(p => p.LastUpdateDate).AsQueryable();

            var filteredEntities = await query
                .Select(product =>
                    _mapper.Map(product, new ProductCategoryDto()))
                .ToListAsync(cancellationToken);

            if (filteredEntities is null)
                throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFoundMessage);

            return new Response<IEnumerable<ProductCategoryDto>>(filteredEntities);
        }
    }
}