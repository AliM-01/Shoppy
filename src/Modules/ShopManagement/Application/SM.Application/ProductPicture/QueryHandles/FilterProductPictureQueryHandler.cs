using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SM.Application.Contracts.ProductPicture.DTOs;
using SM.Application.Contracts.ProductPicture.Queries;
using System.Linq;

namespace SM.Application.ProductPicture.QueryHandles;
public class FilterProductPicturesQueryHandler : IRequestHandler<FilterProductPicturesQuery, Response<FilterProductPictureDto>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ProductPicture.ProductPicture> _productPictureRepository;
    private readonly IMapper _mapper;

    public FilterProductPicturesQueryHandler(IGenericRepository<Domain.ProductPicture.ProductPicture> productPictureRepository, IMapper mapper)
    {
        _productPictureRepository = Guard.Against.Null(productPictureRepository, nameof(_productPictureRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<FilterProductPictureDto>> Handle(FilterProductPicturesQuery request, CancellationToken cancellationToken)
    {
        var query = _productPictureRepository.GetQuery()
            .OrderByDescending(p => p.LastUpdateDate).AsQueryable();

        #region filter

        if (request.Filter.ProductId != 0)
            query = query.Where(s => s.ProductId == request.Filter.ProductId);

        #endregion filter

        #region paging

        var filteredEntities = await query
            .Select(product =>
                _mapper.Map(product, new ProductPictureDto()))
            .ToListAsync(cancellationToken);

        #endregion paging

        if (filteredEntities is null)
            throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFoundMessage);

        request.Filter.ProductPictures = filteredEntities;

        return new Response<FilterProductPictureDto>(request.Filter);
    }
}