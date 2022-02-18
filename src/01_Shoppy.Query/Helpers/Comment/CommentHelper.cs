using _01_Shoppy.Query.Models.Comment;
using AutoMapper;

namespace _01_Shoppy.Query.Helpers.Comment;

public static class CommentHelper
{
    public static List<CommentQueryModel> MapComments(this List<CM.Domain.Comment.Comment> comments, IMapper mapper)
    {
        return comments
            .OrderByDescending(x => x.LastUpdateDate)
            .Select(c =>
                mapper.Map(c, new CommentQueryModel()))
            .ToList();
    }
}
