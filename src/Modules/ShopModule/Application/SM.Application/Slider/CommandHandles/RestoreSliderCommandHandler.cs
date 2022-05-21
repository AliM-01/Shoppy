﻿using SM.Application.Contracts.Slider.Commands;

namespace SM.Application.Slider.CommandHandles;

public class RestoreSliderCommandHandler : IRequestHandler<RestoreSliderCommand, ApiResult>
{
    #region Ctor

    private readonly IRepository<Domain.Slider.Slider> _sliderRepository;

    public RestoreSliderCommandHandler(IRepository<Domain.Slider.Slider> sliderRepository)
    {
        _sliderRepository = Guard.Against.Null(sliderRepository, nameof(_sliderRepository));
    }

    #endregion

    public async Task<ApiResult> Handle(RestoreSliderCommand request, CancellationToken cancellationToken)
    {
        var slider = await _sliderRepository.FindByIdAsync(request.SliderId);

        NotFoundApiException.ThrowIfNull(slider);

        slider.IsDeleted = false;

        await _sliderRepository.UpdateAsync(slider);

        return ApiResponse.Success(ApplicationErrorMessage.RecordDeleted);
    }
}