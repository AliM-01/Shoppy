using DM.Application.Contracts.DiscountCode.Commands;
using MongoDB.Driver;

namespace DM.Application.DiscountCode.CommandHandles;

public class EditDiscountCodeCommandHandler : IRequestHandler<EditDiscountCodeCommand, Response<string>>
{
    #region Ctor

    private readonly IRepository<Domain.DiscountCode.DiscountCode> _discountCodeRepository;
    private readonly IMapper _mapper;

    public EditDiscountCodeCommandHandler(IRepository<Domain.DiscountCode.DiscountCode> discountCodeRepository, IMapper mapper)
    {
        _discountCodeRepository = Guard.Against.Null(discountCodeRepository, nameof(_discountCodeRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));

    }

    #endregion

    public async Task<Response<string>> Handle(EditDiscountCodeCommand request, CancellationToken cancellationToken)
    {
        var discountCode = await _discountCodeRepository.GetByIdAsync(request.DiscountCode.Id);

        if (discountCode is null)
            throw new NotFoundApiException();

        var filter = Builders<Domain.DiscountCode.DiscountCode>.Filter.Eq(x => x.Code, request.DiscountCode.Code);
        var existsDiscount = await _discountCodeRepository.GetByFilter(filter);

        if (existsDiscount is not null && existsDiscount.Id != discountCode.Id)
            throw new ApiException("برای کد قبلا تخفیف در نظر گرفته شده است");

        _mapper.Map(request.DiscountCode, discountCode);

        await _discountCodeRepository.UpdateAsync(discountCode);

        return new Response<string>();
    }
}