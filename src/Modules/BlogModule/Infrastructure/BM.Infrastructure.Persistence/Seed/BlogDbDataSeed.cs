using _0_Framework.Application.Extensions;
using _0_Framework.Infrastructure;
using BM.Domain.Article;
using BM.Domain.ArticleCategory;
using BM.Infrastructure.Persistence.Settings;
using MongoDB.Driver;

namespace BM.Infrastructure.Persistence.Seed;

public static class BlogDbDataSeed
{
    public static ArticleCategory[] SeedArticleCategoryData(BlogDbSettings dbSettings)
    {
        var collection = DbConnection.Conncet<ArticleCategory>(dbSettings);

        bool existsAny = collection.Find(_ => true).Any();

        if (!existsAny)
        {
            const string description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ";

            ArticleCategory[] data =
            {
                new ArticleCategory
                {
                    Title = "اخبار شرکت",
                    OrderShow = 1,
                    Slug = "اخبار شرکت".ToSlug(),
                    Description = description,
                    CanonicalAddress = "",
                    ImagePath = "f07f34e1d58f494fb93c2316b7001ccb.png",
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
                    ImagePath = "d410719e5f574b3cbab5239bbe2484c2.png",
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
                    ImagePath = "e28902fd1e6b4969823956f6e3f65e07.png",
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

    public static void SeedArticleData(BlogDbSettings dbSettings, ArticleCategory[] categories)
    {
        if (categories is null)
            return;

        var collection = DbConnection.Conncet<Article>(dbSettings);

        bool existsAny = collection.Find(_ => true).Any();

        if (!existsAny)
        {
            const string summary = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ";
            const string text = "<p style='font-weight: normal;text-align: right;'>لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ، و با استفاده از طراحان گرافیک است، چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است، و برای شرایط فعلی تکنولوژی مورد نیاز، و کاربردهای متنوع با هدف بهبود ابزارهای کاربردی می باشد، کتابهای زیادی در شصت و سه درصد گذشته حال و آینده، شناخت فراوان جامعه و متخصصان را می طلبد، تا با نرم افزارها شناخت بیشتری را برای طراحان رایانه ای علی الخصوص طراحان خلاقی، و فرهنگ پیشرو در زبان فارسی ایجاد کرد، در این صورت می توان امید داشت که تمام و دشواری موجود در ارائه راهکارها، و شرایط سخت تایپ به پایان رسد و زمان مورد نیاز شامل حروفچینی دستاوردهای اصلی، و جوابگوی سوالات پیوسته اهل دنیای موجود طراحی اساسا مورد استفاده قرار گیرد.</p>";

            Article[] data =
            {
                new Article
                {
                    Title = "مقاله 1",
                    Summary = summary,
                    Text = text,
                    Slug = "مقاله 1".ToSlug(),
                    CanonicalAddress = "",
                    ImagePath = "624b14d0bb13471f9fa00b2065829574.jpg",
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
                    ImagePath = "5ae53945c308429f8c1712ac33a9e12c.jpg",
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
                    ImagePath = "453b8f1331e647e3944b5dc726756612.jpg",
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

