using AutoMapper;
using DM.Application.Contracts.ColleagueDiscount.Commands;
using SM.Domain.Product;

namespace DM.Application.ColleagueDiscount.CommandHandles;

public class EditColleagueDiscountCommandHandler : IRequestHandler<EditColleagueDiscountCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ColleagueDiscount.ColleagueDiscount> _colleagueDiscountRepository;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Product> _productRepository;

    public EditColleagueDiscountCommandHandler(IGenericRepository<Domain.ColleagueDiscount.ColleagueDiscount> colleagueDiscountRepository,
         IGenericRepository<Product> productRepository, IMapper mapper)
    {
        _colleagueDiscountRepository = Guard.Against.Null(_colleagueDiscountRepository, nameof(colleagueDiscountRepository));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));

    }

    #endregion

    public async Task<Response<string>> Handle(EditColleagueDiscountCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetEntityById(request.ColleagueDiscount.ProductId);

        if (product is null)
            throw new NotFoundApiException("محصولی با این شناسه پیدا نشد");

        var colleagueDiscount = await _colleagueDiscountRepository.GetEntityById(request.ColleagueDiscount.Id);

        if (colleagueDiscount is null)
            throw new NotFoundApiException();

        _mapper.Map(request.ColleagueDiscount, colleagueDiscount);

        _colleagueDiscountRepository.Update(colleagueDiscount);
        await _colleagueDiscountRepository.SaveChanges();

        return new Response<string>();
    }
}