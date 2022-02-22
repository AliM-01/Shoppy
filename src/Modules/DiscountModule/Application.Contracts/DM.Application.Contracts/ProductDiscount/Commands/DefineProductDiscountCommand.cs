using DM.Application.Contracts.ProductDiscount.DTOs;

namespace DM.Application.Contracts.ProductDiscount.Commands;

public record DefineProductDiscountCommand
    (DefineProductDiscountDto ProductDiscount) : IRequest<Response<string>>;