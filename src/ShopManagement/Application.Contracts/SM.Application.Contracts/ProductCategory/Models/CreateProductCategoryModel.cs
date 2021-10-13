namespace SM.Application.Contracts.ProductCategory.Models
{
    public class CreateProductCategoryModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public string ImageAlt { get; set; }

        public string ImageTitle { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public string Slug { get; set; }
    }
}