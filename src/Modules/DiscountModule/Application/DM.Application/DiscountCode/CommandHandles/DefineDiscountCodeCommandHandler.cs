using DM.Application.Contracts.DiscountCode.Commands;
using MongoDB.Driver;

namespace DM.Application.DiscountCode.CommandHandles;

public class DefineDiscountCodeCommandHandler : IRequestHandler<DefineDiscountCodeCommand, ApiResult>
{
    #region Ctor

    private readonly IRepository<Domain.DiscountCode.DiscountCode> _discountCodeRepository;
    private readonly IMapper _mapper;

    public DefineDiscountCodeCommandHandler(IRepository<Domain.DiscountCode.DiscountCode> discountCodeRepository, IMapper mapper)
    {
        _discountCodeRepository = Guard.Against.Null(discountCodeRepository, nameof(_discountCodeRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));

    }

    #endregion

    public async Task<ApiResult> Handle(DefineDiscountCodeCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.DiscountCode.DiscountCode>.Filter.Eq(x => x.Code, request.DiscountCode.Code);
        var existsDiscount = await _discountCodeRepository.GetByFilter(filter);

        if (existsDiscount is not null)
            throw new ApiException("برای کد قبلا تخفیف در نظر گرفته شده است");

        var discountCode =
            _mapper.Map(request.DiscountCode, new Domain.DiscountCode.DiscountCode());

        await _discountCodeRepository.InsertAsync(discountCode);

        return ApiResponse.Success();
    }
}