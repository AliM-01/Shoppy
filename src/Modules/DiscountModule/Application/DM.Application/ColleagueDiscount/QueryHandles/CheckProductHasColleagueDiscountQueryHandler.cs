using DM.Application.Contracts.ColleagueDiscount.DTOs;
using DM.Application.Contracts.ColleagueDiscount.Queries;
using SM.Domain.Product;

namespace DM.Application.ColleagueDiscount.QueryHandles;

public class CheckProductHasColleagueDiscountQueryHandler : IRequestHandler<CheckProductHasColleagueDiscountQuery, Response<CheckProductHasColleagueDiscountResponseDto>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ColleagueDiscount.ColleagueDiscount> _colleagueDiscountRepository;
    private readonly IGenericRepository<Product> _productRepository;

    public CheckProductHasColleagueDiscountQueryHandler(IGenericRepository<Domain.ColleagueDiscount.ColleagueDiscount> colleagueDiscountRepository,
        IGenericRepository<Product> productRepository)
    {
        _colleagueDiscountRepository = Guard.Against.Null(colleagueDiscountRepository, nameof(_colleagueDiscountRepository));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
    }

    #endregion

    public async Task<Response<CheckProductHasColleagueDiscountResponseDto>> Handle(CheckProductHasColleagueDiscountQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetEntityById(request.ProductId);

        if (product is null)
            throw new NotFoundApiException("محصولی با این شناسه پیدا نشد");

        bool existsColleagueDiscount = _colleagueDiscountRepository.Exists(x => x.ProductId == request.ProductId);

        if (existsColleagueDiscount)
            return new Response<CheckProductHasColleagueDiscountResponseDto>(
                new CheckProductHasColleagueDiscountResponseDto { ExistsColleagueDiscount = true });

        return new Response<CheckProductHasColleagueDiscountResponseDto>(
                new CheckProductHasColleagueDiscountResponseDto { ExistsColleagueDiscount = false });

    }
}