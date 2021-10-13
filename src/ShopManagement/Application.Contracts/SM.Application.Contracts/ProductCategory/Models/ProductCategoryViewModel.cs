namespace SM.Application.Contracts.ProductCategory.Models
{
    public class ProductCategoryViewModel
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string ImagePath { get; set; }

        public string CreationDate { get; set; }

        public long ProductsCount { get; set; }
    }
}