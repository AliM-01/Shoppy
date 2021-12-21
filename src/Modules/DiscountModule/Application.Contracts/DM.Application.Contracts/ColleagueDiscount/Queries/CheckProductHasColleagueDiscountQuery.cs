using DM.Application.Contracts.ColleagueDiscount.DTOs;

namespace DM.Application.Contracts.ColleagueDiscount.Queries;

public record CheckProductHasColleagueDiscountQuery(long ProductId)
    : IRequest<Response<CheckProductHasColleagueDiscountResponseDto>>;