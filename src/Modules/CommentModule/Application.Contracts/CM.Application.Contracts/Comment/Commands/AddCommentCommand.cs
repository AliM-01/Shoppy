
using CM.Application.Contracts.Comment.DTOs;

namespace CM.Application.Contracts.Comment.Commands;

public record AddCommentCommand(AddCommentDto Comment) : IRequest<Response<string>>;