namespace _01_Shoppy.Query.Contracts.Comment;

public interface ICommentQuery
{
    Task<Response<List<CommentQueryModel>>> GetRecordCommentsById(long recordId);
}
