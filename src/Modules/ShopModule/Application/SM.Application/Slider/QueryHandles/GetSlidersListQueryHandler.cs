using Microsoft.EntityFrameworkCore;
using SM.Application.Contracts.Slider.DTOs;
using SM.Application.Contracts.Slider.Queries;
using System.Collections.Generic;
using System.Linq;

namespace SM.Application.Slider.QueryHandles;
public class GetSlidersListQueryHandler : IRequestHandler<GetSlidersListQuery, Response<IEnumerable<SliderDto>>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Slider.Slider> _sliderRepository;
    private readonly IMapper _mapper;

    public GetSlidersListQueryHandler(IGenericRepository<Domain.Slider.Slider> sliderRepository, IMapper mapper)
    {
        _sliderRepository = Guard.Against.Null(sliderRepository, nameof(_sliderRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<IEnumerable<SliderDto>>> Handle(GetSlidersListQuery request, CancellationToken cancellationToken)
    {
        var query = _sliderRepository.GetQuery()
            .OrderByDescending(p => p.LastUpdateDate).AsQueryable();

        var sliders = await query
            .Select(product => _mapper.Map(product, new SliderDto()))
            .ToListAsync(cancellationToken);

        if (sliders is null)
            throw new ApiException(ApplicationErrorMessage.RecordNotFoundMessage);

        return new Response<IEnumerable<SliderDto>>(sliders);
    }
}