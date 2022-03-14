namespace _0_Framework.Application.Models.Paging;

public static class Pager
{
    public static BasePaging BuildPager(this BasePaging page, int allPagesCount, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

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