﻿using DM.Application.Contracts.ProductDiscount.Commands;
using DM.Application.Contracts.Sevices;

namespace DM.Application.ProductDiscount.CommandHandles;

public class EditProductDiscountCommandHandler : IRequestHandler<EditProductDiscountCommand, ApiResult>
{
    #region Ctor

    private readonly IRepository<Domain.ProductDiscount.ProductDiscount> _productDiscountRepository;
    private readonly IMapper _mapper;
    private readonly IDMProucAclService _productAcl;

    public EditProductDiscountCommandHandler(IRepository<Domain.ProductDiscount.ProductDiscount> productDiscountRepository,
         IDMProucAclService productAcl, IMapper mapper)
    {
        _productDiscountRepository = Guard.Against.Null(productDiscountRepository, nameof(_productDiscountRepository));
        _productAcl = Guard.Against.Null(productAcl, nameof(_productAcl));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));

    }

    #endregion

    public async Task<ApiResult> Handle(EditProductDiscountCommand request, CancellationToken cancellationToken)
    {
        if (!(await _productAcl.ExistsProduct(request.ProductDiscount.ProductId)))
            throw new NotFoundApiException("محصولی با این شناسه پیدا نشد");

        var productDiscount = await _productDiscountRepository.FindByIdAsync(request.ProductDiscount.Id);

        NotFoundApiException.ThrowIfNull(productDiscount);

        _mapper.Map(request.ProductDiscount, productDiscount);

        await _productDiscountRepository.UpdateAsync(productDiscount);

        return ApiResponse.Success();
    }
}