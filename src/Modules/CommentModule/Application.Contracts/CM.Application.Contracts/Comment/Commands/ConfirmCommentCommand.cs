namespace CM.Application.Contracts.Comment.Commands;

public record ConfirmCommentCommand(long CommentId) : IRequest<Response<string>>;