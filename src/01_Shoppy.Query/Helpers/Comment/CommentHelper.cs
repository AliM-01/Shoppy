using _01_Shoppy.Query.Models.Comment;
using AutoMapper;

namespace _01_Shoppy.Query.Helpers.Comment;

public static class CommentHelper
{
    public static IQueryable<CommentQueryModel> MapComments(this IQueryable<CM.Domain.Comment.Comment> comments, IMapper mapper)
    {
        return comments
            .OrderByDescending(x => x.LastUpdateDate)
            .Select(c =>
                mapper.Map(c, new CommentQueryModel()));
    }
}
