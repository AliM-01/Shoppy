namespace _0_Framework.Application.Models.Paging;
public class Pager
{
    public static BasePaging Build(int pageId, int allPagesCount, int take, int shownPages)
    {
        var pageCount = Convert.ToInt32(Math.Ceiling(allPagesCount / (double)take));
        return new BasePaging
        {
            PageId = pageId,
            AllPagesCount = allPagesCount,
            TakePage = take,
            SkipPage = (pageId - 1) * take,
            StartPage = pageId - shownPages <= 0 ? 1 : pageId - shownPages,
            EndPage = pageId + shownPages > pageCount ? pageCount : pageId + shownPages,
            ShownPages = shownPages,
            PageCount = pageCount
        };
    }
}

public static class PagerExtension
{
    public static BasePaging BuildPager(this BasePaging page, int allPagesCount)
    {
        var pageCount = Convert.ToInt32(Math.Ceiling(allPagesCount / (double)page.TakePage));
        return new BasePaging
        {
            PageId = page.PageId,
            AllPagesCount = allPagesCount,
            TakePage = page.TakePage,
            SkipPage = (page.PageId - 1) * page.TakePage,
            StartPage = page.PageId - page.ShownPages <= 0 ? 1 : page.PageId - page.ShownPages,
            EndPage = page.PageId + page.ShownPages > pageCount ? pageCount : page.PageId + page.ShownPages,
            ShownPages = page.ShownPages,
            PageCount = pageCount
        };
    }
}