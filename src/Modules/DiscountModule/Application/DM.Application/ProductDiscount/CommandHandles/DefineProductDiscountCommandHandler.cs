using DM.Application.Contracts.ProductDiscount.Commands;
using DM.Application.Contracts.Sevices;

namespace DM.Application.ProductDiscount.CommandHandles;

public class DefineProductDiscountCommandHandler : IRequestHandler<DefineProductDiscountCommand, Response<string>>
{
    #region Ctor

    private readonly IRepository<Domain.ProductDiscount.ProductDiscount> _productDiscountRepository;
    private readonly IMapper _mapper;
    private readonly IDMProucAclService _productAcl;

    public DefineProductDiscountCommandHandler(IRepository<Domain.ProductDiscount.ProductDiscount> productDiscountRepository,
         IDMProucAclService productAcl, IMapper mapper)
    {
        _productDiscountRepository = Guard.Against.Null(productDiscountRepository, nameof(_productDiscountRepository));
        _productAcl = Guard.Against.Null(productAcl, nameof(_productAcl));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));

    }

    #endregion

    public async Task<Response<string>> Handle(DefineProductDiscountCommand request, CancellationToken cancellationToken)
    {
        if (await _productAcl.ExistsProductDiscount(request.ProductDiscount.ProductId))
            throw new ApiException("برای این محصول قبلا تخفیف در نظر گرفته شده است");

        var productDiscount =
            _mapper.Map(request.ProductDiscount, new Domain.ProductDiscount.ProductDiscount());

        await _productDiscountRepository.InsertAsync(productDiscount);

        return new Response<string>(ApplicationErrorMessage.OperationSuccedded);
    }
}