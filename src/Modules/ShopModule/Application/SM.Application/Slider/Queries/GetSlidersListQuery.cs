using SM.Application.Slider.DTOs;
using SM.Application.Slider.Queries;
using System.Collections.Generic;
using System.Linq;

namespace SM.Application.Slider.Queries;
public record GetSlidersListQuery : IRequest<IEnumerable<SliderDto>>;

public class GetSlidersListQueryHandler : IRequestHandler<GetSlidersListQuery, IEnumerable<SliderDto>>
{
    private readonly IRepository<Domain.Slider.Slider> _sliderRepository;
    private readonly IMapper _mapper;

    public GetSlidersListQueryHandler(IRepository<Domain.Slider.Slider> sliderRepository, IMapper mapper)
    {
        _sliderRepository = Guard.Against.Null(sliderRepository, nameof(_sliderRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public Task<IEnumerable<SliderDto>> Handle(GetSlidersListQuery request, CancellationToken cancellationToken)
    {
        var query = _sliderRepository.AsQueryable(false, cancellationToken);

        var sliders = _sliderRepository
            .AsQueryable(false, cancellationToken)
            .OrderByDescending(p => p.LastUpdateDate)
            .ToList()
            .Select(product => _mapper.Map(product, new SliderDto()));

        if (sliders is null)
            throw new NoContentApiException();

        return Task.FromResult(sliders);
    }
}