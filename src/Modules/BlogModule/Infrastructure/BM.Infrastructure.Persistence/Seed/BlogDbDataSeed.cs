using _0_Framework.Application.Extensions;
using BM.Domain.Article;
using BM.Domain.ArticleCategory;
using MongoDB.Driver;

namespace BM.Infrastructure.Persistence.Seed;

public static class BlogDbDataSeed
{
    public static ArticleCategory[] SeedArticleCategoryData(IMongoCollection<ArticleCategory> collection)
    {
        bool existsAny = collection.Find(_ => true).Any();

        const string description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ";
        if (!existsAny)
        {
            ArticleCategory[] data =
            {
                new ArticleCategory
                {
                    Title = "اخبار شرکت",
                    OrderShow = 1,
                    Slug = "اخبار شرکت".ToSlug(),
                    Description = description,
                    CanonicalAddress = "",
                    ImagePath = "article_01.jpg",
                    ImageAlt = "اخبار شرکت",
                    ImageTitle = "اخبار شرکت",
                    MetaKeywords = "اخبار-شرکت",
                    MetaDescription = description
                },
                new ArticleCategory
                {
                    Title = "اطلاعیه",
                    OrderShow = 2,
                    Slug = "اطلاعیه".ToSlug(),
                    Description = description,
                    CanonicalAddress = "",
                    ImagePath = "article_02.jpg",
                    ImageAlt = "اطلاعیه",
                    ImageTitle = "اطلاعیه",
                    MetaKeywords = "اطلاعیه",
                    MetaDescription = description
                },
                new ArticleCategory
                {
                    Title = "فروشگاه",
                    OrderShow = 3,
                    Slug = "فروشگاه".ToSlug(),
                    Description = description,
                    CanonicalAddress = "",
                    ImagePath = "article_03.jpg",
                    ImageAlt = "فروشگاه",
                    ImageTitle = "فروشگاه",
                    MetaKeywords = "فروشگاه",
                    MetaDescription = description
                }
            };
            collection.InsertMany(data);

            return data;
        }

        return null;
    }

    public static void SeedArticleData(IMongoCollection<Article> collection, ArticleCategory[] categories)
    {
        bool existsAny = collection.Find(_ => true).Any();

        const string summary = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ";
        const string text = "<p style='font-weight: normal;text-align: right;'>لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ، و با استفاده از طراحان گرافیک است، چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است، و برای شرایط فعلی تکنولوژی مورد نیاز، و کاربردهای متنوع با هدف بهبود ابزارهای کاربردی می باشد، کتابهای زیادی در شصت و سه درصد گذشته حال و آینده، شناخت فراوان جامعه و متخصصان را می طلبد، تا با نرم افزارها شناخت بیشتری را برای طراحان رایانه ای علی الخصوص طراحان خلاقی، و فرهنگ پیشرو در زبان فارسی ایجاد کرد، در این صورت می توان امید داشت که تمام و دشواری موجود در ارائه راهکارها، و شرایط سخت تایپ به پایان رسد و زمان مورد نیاز شامل حروفچینی دستاوردهای اصلی، و جوابگوی سوالات پیوسته اهل دنیای موجود طراحی اساسا مورد استفاده قرار گیرد.</p>";

        if (!existsAny)
        {
            Article[] data =
            {
                new Article
                {
                    Title = "مقاله 1",
                    Summary = summary,
                    Text = text,
                    Slug = "مقاله 1".ToSlug(),
                    CanonicalAddress = "",
                    ImagePath = "",
                    ImageAlt = "مقاله 1",
                    ImageTitle = "مقاله 1",
                    MetaKeywords = "مقاله 1",
                    MetaDescription = summary,
                    CategoryId = categories[0].Id,
                    Category = categories[0]
                },
                new Article
                {
                    Title = "مقاله 2",
                    Summary = summary,
                    Text = text,
                    Slug = "مقاله 2".ToSlug(),
                    CanonicalAddress = "",
                    ImagePath = "",
                    ImageAlt = "مقاله 2",
                    ImageTitle = "مقاله 2",
                    MetaKeywords = "مقاله 2",
                    MetaDescription = summary,
                    CategoryId = categories[1].Id,
                    Category = categories[1]
                },
                new Article
                {
                    Title = "مقاله 3",
                    Summary = summary,
                    Text = text,
                    Slug = "مقاله 3".ToSlug(),
                    CanonicalAddress = "",
                    ImagePath = "",
                    ImageAlt = "مقاله 3",
                    ImageTitle = "مقاله 3",
                    MetaKeywords = "مقاله 3",
                    MetaDescription = summary,
                    CategoryId = categories[2].Id,
                    Category = categories[2]
                }
            };

            collection.InsertMany(data);
        }
    }

}

