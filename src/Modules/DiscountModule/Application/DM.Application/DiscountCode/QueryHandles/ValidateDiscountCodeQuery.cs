using _01_Shoppy.Query.Models.Discount;
using Microsoft.AspNetCore.WebUtilities;
using MongoDB.Driver;
using System.Text;

namespace DM.Application.DiscountCode.QueryHandles;

public record ValidateDiscountCodeQuery(string Code) : IRequest<ValidateDiscountCodeResponseDto>;

public class ValidateDiscountCodeQueryHandler : IRequestHandler<ValidateDiscountCodeQuery, ValidateDiscountCodeResponseDto>
{
    private readonly IRepository<Domain.DiscountCode.DiscountCode> _discountCodeRepository;
    private readonly IMapper _mapper;

    public ValidateDiscountCodeQueryHandler(
        IRepository<Domain.DiscountCode.DiscountCode> discountCodeRepository, IMapper mapper)
    {
        _discountCodeRepository = Guard.Against.Null(discountCodeRepository, nameof(_discountCodeRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<ValidateDiscountCodeResponseDto> Handle(ValidateDiscountCodeQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var filter = Builders<Domain.DiscountCode.DiscountCode>.Filter.Eq(x => x.Code, request.Code);
        var discount = await _discountCodeRepository.FindOne(filter, cancellationToken);

        NotFoundApiException.ThrowIfNull(discount, "تخفیفی با این کد پیدا نشد");

        var mappedDiscount = _mapper.Map<ValidateDiscountCodeResponseDto>(discount);
        mappedDiscount.Id = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(discount.Id));

        #region Untill Expiration

        var untillExpirationTimeSpan = discount.EndDate - discount.StartDate;

        if (untillExpirationTimeSpan <= TimeSpan.FromDays(7))
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

        #endregion

        return mappedDiscount;
    }
}
