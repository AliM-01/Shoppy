using DM.Application.Contracts.ColleagueDiscount.Commands;
using SM.Domain.Product;

namespace DM.Application.ColleagueDiscount.CommandHandles;

public class EditColleagueDiscountCommandHandler : IRequestHandler<EditColleagueDiscountCommand, Response<string>>
{
    #region Ctor

    private readonly IMongoHelper<Domain.ColleagueDiscount.ColleagueDiscount> _colleagueDiscountHelper;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Product> _productRepository;

    public EditColleagueDiscountCommandHandler(IMongoHelper<Domain.ColleagueDiscount.ColleagueDiscount> colleagueDiscountHelper,
         IGenericRepository<Product> productRepository, IMapper mapper)
    {
        _colleagueDiscountHelper = Guard.Against.Null(colleagueDiscountHelper, nameof(_colleagueDiscountHelper));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));

    }

    #endregion

    public async Task<Response<string>> Handle(EditColleagueDiscountCommand request, CancellationToken cancellationToken)
    {
        var existsProduct = _productRepository.Exists(p => p.Id == request.ColleagueDiscount.ProductId);

        if (!existsProduct)
            throw new NotFoundApiException("محصولی با این شناسه پیدا نشد");

        var colleagueDiscount = await _colleagueDiscountHelper.GetByIdAsync(request.ColleagueDiscount.Id);

        if (colleagueDiscount is null)
            throw new NotFoundApiException();

        _mapper.Map(request.ColleagueDiscount, colleagueDiscount);

        await _colleagueDiscountHelper.UpdateAsync(colleagueDiscount);

        return new Response<string>();
    }
}