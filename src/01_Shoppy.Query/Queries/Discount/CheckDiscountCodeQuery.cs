using _01_Shoppy.Query.Models.Discount;

namespace _01_Shoppy.Query.Queries.Discount;

public record CheckDiscountCodeQuery(string Code) : IRequest<ApiResult<CheckDiscountCodeResponseDto>>;

public class CheckDiscountCodeQueryHandler : IRequestHandler<CheckDiscountCodeQuery, ApiResult<CheckDiscountCodeResponseDto>>
{
    #region Ctor

    private readonly IRepository<DM.Domain.DiscountCode.DiscountCode> _discountCodeRepository;
    private readonly IMapper _mapper;

    public CheckDiscountCodeQueryHandler(
        IRepository<DM.Domain.DiscountCode.DiscountCode> discountCodeRepository, IMapper mapper)
    {
        _discountCodeRepository = Guard.Against.Null(discountCodeRepository, nameof(_discountCodeRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<ApiResult<CheckDiscountCodeResponseDto>> Handle(CheckDiscountCodeQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var filter = Builders<DM.Domain.DiscountCode.DiscountCode>.Filter.Eq(x => x.Code, request.Code);
        var discount = await _discountCodeRepository.GetByFilter(filter, cancellationToken);

        if (discount is null)
            throw new NotFoundApiException("تخفیفی با این کد پیدا نشد");

        var mappedDiscount = _mapper.Map<CheckDiscountCodeResponseDto>(discount);

        #region Untill Expiration

        var untillExpirationTimeSpan = (discount.StartDate - discount.EndDate);

        if (untillExpirationTimeSpan <= TimeSpan.FromDays(7))
        {
            if (untillExpirationTimeSpan <= TimeSpan.FromHours(2))
            {
                mappedDiscount.UntillExpiration = untillExpirationTimeSpan.Hours;
                mappedDiscount.UntillExpirationType = "ساعت";
            }

            if (untillExpirationTimeSpan <= TimeSpan.FromMinutes(59))
            {
                mappedDiscount.UntillExpiration = untillExpirationTimeSpan.Minutes;
                mappedDiscount.UntillExpirationType = "دقیقه";
            }

            if (untillExpirationTimeSpan <= TimeSpan.FromSeconds(120))
            {
                mappedDiscount.UntillExpiration = untillExpirationTimeSpan.Minutes;
                mappedDiscount.UntillExpirationType = "ثانیه";
            }

            mappedDiscount.UntillExpiration = untillExpirationTimeSpan.Days;
            mappedDiscount.UntillExpirationType = "روز";
        }

        #endregion

        return ApiResponse.Success<CheckDiscountCodeResponseDto>(mappedDiscount);
    }
}
