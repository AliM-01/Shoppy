using DM.Application.Contracts.ProductDiscount.DTOs;

namespace DM.Application.Contracts.ProductDiscount.Queries;

public record GetProductDiscountDetailsQuery(string Id) : IRequest<EditProductDiscountDto>;