using DM.Application.Contracts.ProductDiscount.DTOs;

namespace DM.Application.Contracts.ProductDiscount.Commands;

public record EditProductDiscountCommand
    (EditProductDiscountDto ProductDiscount) : IRequest<Response<string>>;