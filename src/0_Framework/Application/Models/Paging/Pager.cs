using System;

namespace _0_Framework.Application.Models.Paging
{
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
}