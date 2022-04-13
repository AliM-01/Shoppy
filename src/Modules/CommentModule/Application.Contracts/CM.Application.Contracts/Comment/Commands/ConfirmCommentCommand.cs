namespace CM.Application.Contracts.Comment.Commands;

public record ConfirmCommentCommand(string CommentId) : IRequest<ApiResult>;