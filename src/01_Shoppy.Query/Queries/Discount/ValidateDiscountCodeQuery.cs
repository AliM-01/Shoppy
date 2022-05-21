using _01_Shoppy.Query.Models.Discount;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace _01_Shoppy.Query.Queries.Discount;

public record ValidateDiscountCodeQuery(string Code) : IRequest<ValidateDiscountCodeResponseDto>;

public class ValidateDiscountCodeQueryHandler : IRequestHandler<ValidateDiscountCodeQuery, ValidateDiscountCodeResponseDto>
{
    #region Ctor

    private readonly IRepository<DM.Domain.DiscountCode.DiscountCode> _discountCodeRepository;
    private readonly IMapper _mapper;

    public ValidateDiscountCodeQueryHandler(
        IRepository<DM.Domain.DiscountCode.DiscountCode> discountCodeRepository, IMapper mapper)
    {
        _discountCodeRepository = Guard.Against.Null(discountCodeRepository, nameof(_discountCodeRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<ValidateDiscountCodeResponseDto> Handle(ValidateDiscountCodeQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var filter = Builders<DM.Domain.DiscountCode.DiscountCode>.Filter.Eq(x => x.Code, request.Code);
        var discount = await _discountCodeRepository.FindOne(filter, cancellationToken);

        if (discount is null)
            throw new NotFoundApiException("تخفیفی با این کد پیدا نشد");

        var mappedDiscount = _mapper.Map<ValidateDiscountCodeResponseDto>(discount);
        mappedDiscount.Id = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(discount.Id));

        #region Untill Expiration

        var untillExpirationTimeSpan = (discount.EndDate - discount.StartDate);

        if (untillExpirationTimeSpan <= TimeSpan.FromDays(7))
        {
            if (untillExpirationTimeSpan <= TimeSpan.FromSeconds(120))
            {
                mappedDiscount.UntillExpiration = untillExpirationTimeSpan.Minutes;
                mappedDiscount.UntillExpirationType = "ثانیه";
            }
            else if (untillExpirationTimeSpan <= TimeSpan.FromMinutes(59))
            {
                mappedDiscount.UntillExpiration = untillExpirationTimeSpan.Minutes;
                mappedDiscount.UntillExpirationType = "دقیقه";
            }
            else if (untillExpirationTimeSpan <= TimeSpan.FromHours(2))
            {
                mappedDiscount.UntillExpiration = untillExpirationTimeSpan.Hours;
                mappedDiscount.UntillExpirationType = "ساعت";
            }
            else
            {
                mappedDiscount.UntillExpiration = untillExpirationTimeSpan.Days;
                mappedDiscount.UntillExpirationType = "روز";
            }

        }

        #endregion

        return mappedDiscount;
    }
}
