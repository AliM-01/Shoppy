namespace _0_Framework.Application.Models.Paging
{
    public class BasePaging
    {
        public BasePaging()
        {
            PageId = 1;
            TakePage = 9;
            ShownPages = 3;
        }

        public int PageId { get; set; }

        public int PageCount { get; set; }

        public int AllPagesCount { get; set; }

        public int StartPage { get; set; }

        public int EndPage { get; set; }

        public int TakePage { get; set; }

        public int SkipPage { get; set; }

        public int ShownPages { get; set; }

        public int GetLastPage()
        {
            return (int)System.Math.Ceiling(AllPagesCount / (double)TakePage);
        }

        public string GetCurrentPagingStatus()
        {
            var startItem = 0;

            var endItem = AllPagesCount;

            if (EndPage > 0)
            {
                startItem = (PageId - 1) * TakePage + 1;

                endItem = PageId * TakePage > AllPagesCount ? AllPagesCount : PageId * TakePage;
            }
            return $"نمایش {startItem}-{endItem} از {AllPagesCount}";
        }

        public BasePaging GetCurrentPaging()
        {
            return this;
        }
    }
}