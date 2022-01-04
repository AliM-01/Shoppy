using DM.Application.Contracts.ColleagueDiscount.Commands;
using SM.Domain.Product;

namespace DM.Application.ColleagueDiscount.CommandHandles;

public class DefineColleagueDiscountCommandHandler : IRequestHandler<DefineColleagueDiscountCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ColleagueDiscount.ColleagueDiscount> _colleagueDiscountRepository;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Product> _productRepository;

    public DefineColleagueDiscountCommandHandler(IGenericRepository<Domain.ColleagueDiscount.ColleagueDiscount> colleagueDiscountRepository,
         IGenericRepository<Product> productRepository, IMapper mapper)
    {
        _colleagueDiscountRepository = Guard.Against.Null(colleagueDiscountRepository, nameof(_colleagueDiscountRepository));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));

    }

    #endregion

    public async Task<Response<string>> Handle(DefineColleagueDiscountCommand request, CancellationToken cancellationToken)
    {
        var existsProduct = _productRepository.Exists(p => p.Id == request.Inventory.ProductId);

        if (!existsProduct)
            throw new NotFoundApiException("محصولی با این شناسه پیدا نشد");

        if (_colleagueDiscountRepository.Exists(x => x.ProductId == request.ColleagueDiscount.ProductId))
            throw new ApiException("برای این محصول قبلا تخفیف در نظر گرفته شده است");

        var colleagueDiscount =
            _mapper.Map(request.ColleagueDiscount, new Domain.ColleagueDiscount.ColleagueDiscount());

        await _colleagueDiscountRepository.InsertEntity(colleagueDiscount);
        await _colleagueDiscountRepository.SaveChanges();

        return new Response<string>(ApplicationErrorMessage.OperationSucceddedMessage);
    }
}