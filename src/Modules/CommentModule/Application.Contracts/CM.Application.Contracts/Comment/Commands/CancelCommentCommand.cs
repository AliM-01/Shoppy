namespace CM.Application.Contracts.Comment.Commands;

public record CancelCommentCommand(string CommentId) : IRequest<Response<string>>;