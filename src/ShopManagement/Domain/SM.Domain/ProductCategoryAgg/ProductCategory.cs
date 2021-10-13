using _0_Framework.Domain;

namespace SM.Domain.ProductCategoryAgg
{
    public class ProductCategory : BaseEntity
    {
        public string Title { get; private set; }

        public string Description { get; private set; }

        public string ImagePath { get; private set; }

        public string ImageAlt { get; private set; }

        public string ImageTitle { get; private set; }

        public string MetaKeywords { get; private set; }

        public string MetaDescription { get; private set; }

        public string Slug { get; private set; }

        #region Ctor

        public ProductCategory(string title, string description, string imagePath,
            string imageAlt, string imageTitle, string metaKeywords, string metaDescription, string slug)
        {
            Title = title;
            Description = description;
            ImagePath = imagePath;
            ImageAlt = imageAlt;
            ImageTitle = imageTitle;
            MetaKeywords = metaKeywords;
            MetaDescription = metaDescription;
            Slug = slug;
        }

        #endregion

        #region Edit

        public void Edit(string title, string description, string imagePath,
            string imageAlt, string imageTitle, string metaKeywords, string metaDescription, string slug)
        {
            Title = title;
            Description = description;
            ImagePath = imagePath;
            ImageAlt = imageAlt;
            ImageTitle = imageTitle;
            MetaKeywords = metaKeywords;
            MetaDescription = metaDescription;
            Slug = slug;
        }

        #endregion
    }
}