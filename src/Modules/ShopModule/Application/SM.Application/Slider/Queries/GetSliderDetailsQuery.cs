using FluentValidation;
using SM.Application.Slider.DTOs;
using SM.Application.Slider.Queries;

namespace SM.Application.Slider.Queries;

public record GetSliderDetailsQuery(string Id) : IRequest<EditSliderDto>;

public class GetSliderDetailsQueryValidator : AbstractValidator<GetSliderDetailsQuery>
{
    public GetSliderDetailsQueryValidator()
    {
        RuleFor(p => p.Id)
            .RequiredValidator("شناسه");
    }
}

public class GetSliderDetailsQueryHandler : IRequestHandler<GetSliderDetailsQuery, EditSliderDto>
{
    private readonly IRepository<Domain.Slider.Slider> _sliderRepository;
    private readonly IMapper _mapper;

    public GetSliderDetailsQueryHandler(IRepository<Domain.Slider.Slider> sliderRepository, IMapper mapper)
    {
        _sliderRepository = Guard.Against.Null(sliderRepository, nameof(_sliderRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<EditSliderDto> Handle(GetSliderDetailsQuery request, CancellationToken cancellationToken)
    {
        var slider = await _sliderRepository.FindByIdAsync(request.Id);

        NotFoundApiException.ThrowIfNull(slider);

        return _mapper.Map<EditSliderDto>(slider);
    }
}