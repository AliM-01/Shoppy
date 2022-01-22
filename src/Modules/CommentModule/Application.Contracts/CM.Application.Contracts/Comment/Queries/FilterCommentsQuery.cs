using CM.Application.Contracts.Comment.DTOs;

namespace CM.Application.Contracts.Inventory.Queries;

public record FilterCommentsQuery
    (FilterCommentDto Filter) : IRequest<Response<FilterCommentDto>>;