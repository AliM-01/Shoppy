using DM.Application.Contracts.ColleagueDiscount.DTOs;
using DM.Application.Contracts.ColleagueDiscount.Queries;
using SM.Domain.Product;

namespace DM.Application.ColleagueDiscount.QueryHandles;

public class CheckProductHasColleagueDiscountQueryHandler : IRequestHandler<CheckProductHasColleagueDiscountQuery, Response<CheckProductHasColleagueDiscountResponseDto>>
{
    #region Ctor

    private readonly IMongoHelper<Domain.ColleagueDiscount.ColleagueDiscount> _colleagueDiscountHelper;
    private readonly IGenericRepository<Product> _productRepository;

    public CheckProductHasColleagueDiscountQueryHandler(IMongoHelper<Domain.ColleagueDiscount.ColleagueDiscount> colleagueDiscountHelper,
        IGenericRepository<Product> productRepository)
    {
        _colleagueDiscountHelper = Guard.Against.Null(colleagueDiscountHelper, nameof(_colleagueDiscountHelper));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
    }

    #endregion

    public async Task<Response<CheckProductHasColleagueDiscountResponseDto>> Handle(CheckProductHasColleagueDiscountQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetEntityById(request.ProductId);

        if (product is null)
            throw new NotFoundApiException("محصولی با این شناسه پیدا نشد");

        bool existsColleagueDiscount = await _colleagueDiscountHelper.ExistsAsync(x => x.ProductId == request.ProductId);

        return new Response<CheckProductHasColleagueDiscountResponseDto>(
            new CheckProductHasColleagueDiscountResponseDto
            {
                ExistsColleagueDiscount = existsColleagueDiscount
            });
    }
}