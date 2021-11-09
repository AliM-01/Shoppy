using System.Linq;

namespace _0_Framework.Application.Models.Paging;
public static class PagingExtension
{
    public static IQueryable<T> Paging<T>(this IQueryable<T> query, BasePaging paging)
    {
        return query.Skip(paging.SkipPage).Take(paging.TakePage);
    }
}