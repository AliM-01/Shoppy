namespace CM.Application.Contracts.Comment.Commands;

public record CancelCommentCommand(long CommentId) : IRequest<Response<string>>;