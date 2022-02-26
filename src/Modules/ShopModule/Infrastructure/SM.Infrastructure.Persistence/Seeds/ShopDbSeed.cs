using _0_Framework.Application.Extensions;
using MongoDB.Bson;
using MongoDB.Driver;
using SM.Domain.Product;
using SM.Domain.ProductCategory;
using SM.Domain.ProductFeature;
using SM.Domain.ProductPicture;
using SM.Domain.Slider;
using System.Collections.Generic;

namespace SM.Infrastructure.Persistence.Seeds;

public static class ShopDbSeed
{
    public static ProductCategory[] SeedProductCategories(IMongoCollection<ProductCategory> categories)
    {
        bool existsCategories = categories.Find(_ => true).Any();

        if (!existsCategories)
        {
            ProductCategory[] data =
            {
                new ProductCategory
                {
                    Title = "گوشی موبایل",
                    Slug = "گوشی موبایل".ToSlug(),
                    Description = "گوشی موبایل",
                    ImagePath = "4b6d02a69be24194958684e7dd108a86.jpg",
                    ImageTitle = "گوشی موبایل",
                    ImageAlt = "گوشی موبایل",
                    MetaKeywords = "گوشی موبایل",
                    MetaDescription = "گوشی موبایل"
                },
                new ProductCategory
                {
                    Title = "ساعت هوشمند",
                    Slug = "ساعت هوشمند".ToSlug(),
                    Description = "ساعت هوشمند",
                    ImagePath = "7d0d7077a204402191b8c5d2ddb5311c.jpg",
                    ImageTitle = "ساعت هوشمند",
                    ImageAlt = "ساعت هوشمند",
                    MetaKeywords = "ساعت هوشمند",
                    MetaDescription = "ساعت هوشمند"
                }
            };
            categories.InsertManyAsync(data);

            return data;
        }

        return null;
    }

    public static void SeedProducts(IMongoCollection<Product> products, ProductCategory[] categories)
    {
        bool existsProducts = products.Find(_ => true).Any();

        if (!existsProducts)
        {
            const string mobileTags = "گوشی موبایل-اپل";
            const string watchTags = "ساعت هوشمند-سامسونگ";

            List<Product> productToAdd = new()
            {
                new Product
                {
                    Id = new ObjectId(SeedProductIdConstants.Product_01).ToString(),
                    CategoryId = categories[0].Id,
                    Category = categories[0],
                    Code = Generators.GenerateCode(),
                    Title = "گوشی موبایل اپل مدل iPhone 13 Pro Max ظرفیت 256 گیگابایت و رم 6 گیگابایت",
                    ShortDescription = "گوشی موبایل اپل مدل iPhone 13 Pro Max ظرفیت 256 گیگابایت و رم 6 گیگابایت",
                    Description = "<p>گوشی موبایل اپل مدل iPhone 13 Pro Max ظرفیت 256 گیگابایت و رم 6 گیگابایت</p>",
                    Slug = "گوشی موبایل اپل مدل iPhone 13 Pro Max ظرفیت 256 گیگابایت و رم 6 گیگابایت".ToSlug(),
                    ImagePath = "3bf8cb01458e4ed094be1c8b2505129c.jpg",
                    ImageTitle = "گوشی موبایل اپل مدل iPhone 13 Pro Max ظرفیت 256 گیگابایت و رم 6 گیگابایت",
                    ImageAlt = "گوشی موبایل اپل مدل iPhone 13 Pro Max ظرفیت 256 گیگابایت و رم 6 گیگابایت",
                    MetaDescription = "گوشی موبایل اپل مدل iPhone 13 Pro Max ظرفیت 256 گیگابایت و رم 6 گیگابایت",
                    MetaKeywords = mobileTags,
                },
                new Product
                {
                    Id = new ObjectId(SeedProductIdConstants.Product_02).ToString(),
                    CategoryId = categories[0].Id,
                    Category = categories[0],
                    Code = Generators.GenerateCode(),
                    Title = "گوشی موبایل اپل مدل iPhone SE 2020 ظرفیت 128 گیگابایت",
                    ShortDescription = "گوشی موبایل اپل مدل iPhone SE 2020 ظرفیت 128 گیگابایت",
                    Description = "<p>گوشی موبایل اپل مدل iPhone SE 2020 ظرفیت 128 گیگابایت</p>",
                    Slug = "گوشی موبایل اپل مدل iPhone SE 2020 ظرفیت 128 گیگابایت".ToSlug(),
                    ImagePath = "dd49c449b9a64eadac220f4cf5d46fa3.jpg",
                    ImageTitle = "گوشی موبایل اپل مدل iPhone SE 2020 ظرفیت 128 گیگابایت",
                    ImageAlt = "گوشی موبایل اپل مدل iPhone SE 2020 ظرفیت 128 گیگابایت",
                    MetaDescription = "گوشی موبایل اپل مدل iPhone SE 2020 ظرفیت 128 گیگابایت",
                    MetaKeywords = mobileTags,
                },
                new Product
                {
                    Id = new ObjectId(SeedProductIdConstants.Product_03).ToString(),
                    CategoryId = categories[0].Id,
                    Category = categories[0],
                    Code = Generators.GenerateCode(),
                    Title = "گوشی موبایل سامسونگ مدل Galaxy A71 ظرفیت 128 گیگابایت و رم 8 گیگابایت",
                    ShortDescription = "گوشی موبایل سامسونگ مدل Galaxy A71 ظرفیت 128 گیگابایت و رم 8 گیگابایت",
                    Description = "<p>گوشی موبایل سامسونگ مدل Galaxy A71 ظرفیت 128 گیگابایت و رم 8 گیگابایت</p>",
                    Slug = "گوشی موبایل سامسونگ مدل Galaxy A71 ظرفیت 128 گیگابایت و رم 8 گیگابایت".ToSlug(),
                    ImagePath = "6a27a2f206594f7e9523aa4bbb48a7d1.jpg",
                    ImageTitle = "گوشی موبایل سامسونگ مدل Galaxy A71 ظرفیت 128 گیگابایت و رم 8 گیگابایت",
                    ImageAlt = "گوشی موبایل سامسونگ مدل Galaxy A71 ظرفیت 128 گیگابایت و رم 8 گیگابایت",
                    MetaDescription = "گوشی موبایل سامسونگ مدل Galaxy A71 ظرفیت 128 گیگابایت و رم 8 گیگابایت",
                    MetaKeywords = mobileTags.Replace("اپل", "سامسونگ"),
                },
                new Product
                {
                    Id = new ObjectId(SeedProductIdConstants.Product_04).ToString(),
                    CategoryId = categories[1].Id,
                    Category = categories[1],
                    Code = Generators.GenerateCode(),
                    Title = "ساعت هوشمند سامسونگ مدل Galaxy Watch4 44mm",
                    ShortDescription = "ساعت هوشمند سامسونگ مدل Galaxy Watch4 44mm",
                    Description = "<p>ساعت هوشمند سامسونگ مدل Galaxy Watch4 44mm</p>",
                    Slug = "ساعت هوشمند سامسونگ مدل Galaxy Watch4 44mm".ToSlug(),
                    ImagePath = "253b8f1331e647e3944b5dc726756612.jpg",
                    ImageTitle = "ساعت هوشمند سامسونگ مدل Galaxy Watch4 44mm",
                    ImageAlt = "ساعت هوشمند سامسونگ مدل Galaxy Watch4 44mm",
                    MetaDescription = "ساعت هوشمند سامسونگ مدل Galaxy Watch4 44mm",
                    MetaKeywords = watchTags,
                },
                new Product
                {
                    Id = new ObjectId(SeedProductIdConstants.Product_05).ToString(),
                    CategoryId = categories[0].Id,
                    Category = categories[0],
                    Code = Generators.GenerateCode(),
                    Title = "گوشی موبایل شیائومی مدل Redmi Note 10 pro ظرفیت 128 گیگابایت و رم 8 گیگابایت ",
                    ShortDescription = "گوشی موبایل شیائومی مدل Redmi Note 10 pro ظرفیت 128 گیگابایت و رم 8 گیگابایت ",
                    Description = "<p>گوشی موبایل شیائومی مدل Redmi Note 10 pro ظرفیت 128 گیگابایت و رم 8 گیگابایت </p>",
                    Slug = "گوشی موبایل شیائومی مدل Redmi Note 10 pro ظرفیت 128 گیگابایت و رم 8 گیگابایت ".ToSlug(),
                    ImagePath = "343ed8a1caf6408990bac14cec39399c.jpg",
                    ImageTitle = "گوشی موبایل شیائومی مدل Redmi Note 10 pro ظرفیت 128 گیگابایت و رم 8 گیگابایت ",
                    ImageAlt = "گوشی موبایل شیائومی مدل Redmi Note 10 pro ظرفیت 128 گیگابایت و رم 8 گیگابایت ",
                    MetaDescription = "گوشی موبایل شیائومی مدل Redmi Note 10 pro ظرفیت 128 گیگابایت و رم 8 گیگابایت ",
                    MetaKeywords = mobileTags.Replace("اپل", "سامسونگ"),
                },
                new Product
                {
                    Id = new ObjectId(SeedProductIdConstants.Product_06).ToString(),
                    CategoryId = categories[0].Id,
                    Category = categories[0],
                    Code = Generators.GenerateCode(),
                    Title = "گوشی موبایل اپل مدل iPhone 13 Pro Max ظرفیت 256 گیگابایت و رم 6 گیگابایت",
                    ShortDescription = "گوشی موبایل اپل مدل iPhone 13 Pro Max ظرفیت 256 گیگابایت و رم 6 گیگابایت",
                    Description = "<p>گوشی موبایل اپل مدل iPhone 13 Pro Max ظرفیت 256 گیگابایت و رم 6 گیگابایت</p>",
                    Slug = "گوشی موبایل اپل مدل iPhone 13 Pro Max ظرفیت 256 گیگابایت و رم 6 گیگابایت".ToSlug(),
                    ImagePath = "3bf8cb01458e4ed094be1c8b2505129c.jpg",
                    ImageTitle = "گوشی موبایل اپل مدل iPhone 13 Pro Max ظرفیت 256 گیگابایت و رم 6 گیگابایت",
                    ImageAlt = "گوشی موبایل اپل مدل iPhone 13 Pro Max ظرفیت 256 گیگابایت و رم 6 گیگابایت",
                    MetaDescription = "گوشی موبایل اپل مدل iPhone 13 Pro Max ظرفیت 256 گیگابایت و رم 6 گیگابایت",
                    MetaKeywords = mobileTags,
                },
                new Product
                {
                    Id = new ObjectId(SeedProductIdConstants.Product_07).ToString(),
                    CategoryId = categories[1].Id,
                    Category = categories[1],
                    Code = Generators.GenerateCode(),
                    Title = "ساعت هوشمند سامسونگ مدل Galaxy Watch Active2 44mm",
                    ShortDescription = "ساعت هوشمند سامسونگ مدل Galaxy Watch Active2 44mm",
                    Description = "<p>ساعت هوشمند سامسونگ مدل Galaxy Watch Active2 44mm</p>",
                    Slug = "ساعت هوشمند سامسونگ مدل Galaxy Watch Active2 44mm".ToSlug(),
                    ImagePath = "629beba0d50b4dbe980cd9844fc5633f.jpg",
                    ImageTitle = "ساعت هوشمند سامسونگ مدل Galaxy Watch Active2 44mm",
                    ImageAlt = "ساعت هوشمند سامسونگ مدل Galaxy Watch Active2 44mm",
                    MetaDescription = "ساعت هوشمند سامسونگ مدل Galaxy Watch Active2 44mm",
                    MetaKeywords = watchTags,
                },
                new Product
                {
                    Id = new ObjectId(SeedProductIdConstants.Product_08).ToString(),
                    CategoryId = categories[0].Id,
                    Category = categories[0],
                    Code = Generators.GenerateCode(),
                    Title = "گوشی موبایل اپل مدل iPhone 13 ظرفیت 128 گیگابایت",
                    ShortDescription = "گوشی موبایل اپل مدل iPhone 13 ظرفیت 128 گیگابایت",
                    Description = "<p>گوشی موبایل اپل مدل iPhone 13 ظرفیت 128 گیگابایت</p>",
                    Slug = "گوشی موبایل اپل مدل iPhone 13 ظرفیت 128 گیگابایت".ToSlug(),
                    ImagePath = "561a21295a01467f81a394102896ea82.jpg",
                    ImageTitle = "گوشی موبایل اپل مدل iPhone 13 ظرفیت 128 گیگابایت",
                    ImageAlt = "گوشی موبایل اپل مدل iPhone 13 ظرفیت 128 گیگابایت",
                    MetaDescription = "گوشی موبایل اپل مدل iPhone 13 ظرفیت 128 گیگابایت",
                    MetaKeywords = mobileTags,
                },
                new Product
                {
                    Id = new ObjectId(SeedProductIdConstants.Product_09).ToString(),
                    CategoryId = categories[0].Id,
                    Category = categories[0],
                    Code = Generators.GenerateCode(),
                    Title = "گوشی موبایل سامسونگ Galaxy A42 ظرفیت 128گیگابایت",
                    ShortDescription = "گوشی موبایل سامسونگ Galaxy A42 ظرفیت 128گیگابایت",
                    Description = "<p>گوشی موبایل سامسونگ Galaxy A42 ظرفیت 128گیگابایت</p>",
                    Slug = "گوشی موبایل سامسونگ Galaxy A42 ظرفیت 128گیگابایت".ToSlug(),
                    ImagePath = "365be3dad28e4852bad8dc18f476054a.jpg",
                    ImageTitle = "گوشی موبایل سامسونگ Galaxy A42 ظرفیت 128گیگابایت",
                    ImageAlt = "گوشی موبایل سامسونگ Galaxy A42 ظرفیت 128گیگابایت",
                    MetaDescription = "گوشی موبایل سامسونگ Galaxy A42 ظرفیت 128گیگابایت",
                    MetaKeywords = mobileTags.Replace("اپل", "سامسونگ"),
                },
                new Product
                {
                    Id = new ObjectId(SeedProductIdConstants.Product_10).ToString(),
                    CategoryId = categories[0].Id,
                    Category = categories[0],
                    Code = Generators.GenerateCode(),
                    Title = "گوشی موبایل سامسونگ Galaxy A71 ظرفیت 128 گیگابایت و رم 8 گیگابایت",
                    ShortDescription = "گوشی موبایل سامسونگ Galaxy A71 ظرفیت 128 گیگابایت و رم 8 گیگابایت",
                    Description = "<p>گوشی موبایل سامسونگ Galaxy A71 ظرفیت 128 گیگابایت و رم 8 گیگابایت</p>",
                    Slug = "گوشی موبایل سامسونگ Galaxy A71 ظرفیت 128 گیگابایت و رم 8 گیگابایت".ToSlug(),
                    ImagePath = "7cd0d089f417478893860c72a98e074d.jpg",
                    ImageTitle = "گوشی موبایل سامسونگ Galaxy A71 ظرفیت 128 گیگابایت و رم 8 گیگابایت",
                    ImageAlt = "گوشی موبایل سامسونگ Galaxy A71 ظرفیت 128 گیگابایت و رم 8 گیگابایت",
                    MetaDescription = "گوشی موبایل سامسونگ Galaxy A71 ظرفیت 128 گیگابایت و رم 8 گیگابایت",
                    MetaKeywords = mobileTags.Replace("اپل", "سامسونگ"),
                },
                new Product
                {
                    Id = new ObjectId(SeedProductIdConstants.Product_11).ToString(),
                    CategoryId = categories[1].Id,
                    Category = categories[1],
                    Code = Generators.GenerateCode(),
                    Title = "ساعت هوشمند اپل واچ سری 7 مدل 45mm Aluminum ",
                    ShortDescription = "ساعت هوشمند اپل واچ سری 7 مدل 45mm Aluminum ",
                    Description = "<p>ساعت هوشمند اپل واچ سری 7 مدل 45mm Aluminum </p>",
                    Slug = "ساعت هوشمند اپل واچ سری 7 مدل 45mm Aluminum ".ToSlug(),
                    ImagePath = "fc918c63f7294de3a272abc7086d593b.jpg",
                    ImageTitle = "ساعت هوشمند اپل واچ سری 7 مدل 45mm Aluminum ",
                    ImageAlt = "ساعت هوشمند اپل واچ سری 7 مدل 45mm Aluminum ",
                    MetaDescription = "ساعت هوشمند اپل واچ سری 7 مدل 45mm Aluminum ",
                    MetaKeywords = watchTags.Replace("سامسونگ", "اپل"),
                }
            };
            products.InsertManyAsync(productToAdd);
        }
    }

    public static void SeedProductPictures(IMongoCollection<ProductPicture> productPictures)
    {
        bool existsProductPictures = productPictures.Find(_ => true).Any();

        if (!existsProductPictures)
        {
            ProductPicture[] productPictureToAdd =
            {
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_01,
                    ImagePath = "bd1cb76e2c95463bbbaf887060de4d42.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_02,
                    ImagePath = "f43065884ebf4cdfbeb27c8397beda7b.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_02,
                    ImagePath = "220779cc7b8d48ed9203702421aff972.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_02,
                    ImagePath = "8ded82c898064925b1c6f4fb75c634e1.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_02,
                    ImagePath = "9ae2f0647ae743b4a50d4d05c5f6a9b8.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_03,
                    ImagePath = "6a27a2f206594f7e9523aa4bbb48a7d1.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_03,
                    ImagePath = "eaea56e9c49e43769279f824515998f9.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_03,
                    ImagePath = "84eee894779b4208be7f48c0c60f51e6.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_03,
                    ImagePath = "9afc5bb80a9f4c67a8d14efb295f2aef.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_04,
                    ImagePath = "253b8f1331e647e3944b5dc726756612.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_04,
                    ImagePath = "5ae53945c308429f8c1712ac33a9e12c.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_04,
                    ImagePath = "0d0135265eba4abca20bd279bac8b526.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_04,
                    ImagePath = "c82659f58d624c74af21b4682bca1ca4.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_05,
                    ImagePath = "343ed8a1caf6408990bac14cec39399c.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_05,
                    ImagePath = "d5591c2ff4144db7b53c297644d999f1.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_05,
                    ImagePath = "9ffe4cf04dd6474292b5ef28485d5db3.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_05,
                    ImagePath = "a6bb57c773474a95afb6f27796db86e5.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_05,
                    ImagePath = "75b0e6163a22463db34a252415efb4bb.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_06,
                    ImagePath = "3bf8cb01458e4ed094be1c8b2505129c.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_06,
                    ImagePath = "1e130e85b5c24f0aab812abe56b166db.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_06,
                    ImagePath = "961c59e60e6e47e78bc0f8677823609d.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_06,
                    ImagePath = "a30f261482434cc5a4bba3d22541def3.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_07,
                    ImagePath = "629beba0d50b4dbe980cd9844fc5633f.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_07,
                    ImagePath = "be904d9cf98e43758a73c92ec4850d09.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_07,
                    ImagePath = "cfb7f95288024950ace42b58e8bdcaf9.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_07,
                    ImagePath = "5c49dcc02b0c4484bcc8448774eada6f.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_07,
                    ImagePath = "5523c351fe9141edbd19e94775b1da29.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_08,
                    ImagePath = "561a21295a01467f81a394102896ea82.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_08,
                    ImagePath = "624b14d0bb13471f9fa00b2065829574.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_08,
                    ImagePath = "181d51379ce84081ac465fb631e6f8f2.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_09,
                    ImagePath = "365be3dad28e4852bad8dc18f476054a.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_09,
                    ImagePath = "33e4b2820626493e9b07d30adb9cca7a.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_09,
                    ImagePath = "3197d5b8125748839c161f51fe413ed7.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_10,
                    ImagePath = "7cd0d089f417478893860c72a98e074d.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_10,
                    ImagePath = "65b357b951a946a2a6f89d719c7d8915.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_10,
                    ImagePath = "d28f3ba7afda427ba0e22f02b7bb3195.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_10,
                    ImagePath = "ec75aab6b0ce4a109c167a96c6aebf90.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_11,
                    ImagePath = "fc918c63f7294de3a272abc7086d593b.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_11,
                    ImagePath = "7708f5a416954b76b7c5af5b2fdf79d6.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_11,
                    ImagePath = "3d51704f065346a9a6c2d761b8ccbc5a.jpg"
                },
                new ProductPicture
                {
                    ProductId = SeedProductIdConstants.Product_11,
                    ImagePath = "2022_01_16_22-03_03_71a7.jpg"
                },
            };
            productPictures.InsertManyAsync(productPictureToAdd);
        }
    }

    public static void SeedProductFeatures(IMongoCollection<ProductFeature> productFeatures)
    {
        bool existsProductFeatures = productFeatures.Find(_ => true).Any();

        if (!existsProductFeatures)
        {
            ProductFeature[] productFeatureToAdd =
            {
                new ProductFeature
                {
                    ProductId = SeedProductIdConstants.Product_01,
                    FeatureTitle = "ظرفیت",
                    FeatureValue = "256GB"
                },
                new ProductFeature
                {
                    ProductId = SeedProductIdConstants.Product_02,
                    FeatureTitle = "ظرفیت",
                    FeatureValue = "128GB"
                },
                new ProductFeature
                {
                    ProductId = SeedProductIdConstants.Product_03,
                    FeatureTitle = "ظرفیت",
                    FeatureValue = "128GB"
                },
                new ProductFeature
                {
                    ProductId = SeedProductIdConstants.Product_04,
                    FeatureTitle = "ورژن",
                    FeatureValue = "44mm"
                },
                new ProductFeature
                {
                    ProductId = SeedProductIdConstants.Product_05,
                    FeatureTitle = "ظرفیت",
                    FeatureValue = "128GB"
                },
                new ProductFeature
                {
                    ProductId = SeedProductIdConstants.Product_06,
                    FeatureTitle = "ظرفیت",
                    FeatureValue = "256GB"
                },
                new ProductFeature
                {
                    ProductId = SeedProductIdConstants.Product_07,
                    FeatureTitle = "ورژن",
                    FeatureValue = "44mm"
                },
                new ProductFeature
                {
                    ProductId = SeedProductIdConstants.Product_08,
                    FeatureTitle = "ظرفیت",
                    FeatureValue = "128GB"
                },
                new ProductFeature
                {
                    ProductId = SeedProductIdConstants.Product_09,
                    FeatureTitle = "ظرفیت",
                    FeatureValue = "128GB"
                },
                new ProductFeature
                {
                    ProductId = SeedProductIdConstants.Product_10,
                    FeatureTitle = "ظرفیت",
                    FeatureValue = "128GB"
                },
                new ProductFeature
                {
                    ProductId = SeedProductIdConstants.Product_10,
                    FeatureTitle = "رم",
                    FeatureValue = "8GB"
                },
                new ProductFeature
                {
                    ProductId = SeedProductIdConstants.Product_11,
                    FeatureTitle = "ورژن",
                    FeatureValue = "44mm"
                },
            };
            productFeatures.InsertManyAsync(productFeatureToAdd);
        }
    }

    public static void SeedSliders(IMongoCollection<Slider> sliders)
    {
        bool existsSliders = sliders.Find(_ => true).Any();

        if (!existsSliders)
        {
            Slider[] sliderToAdd =
            {
                new Slider
                {
                    Heading = "اسلایدر #1",
                    Text = "اسلایدر #1",
                    BtnLink = "/products",
                    BtnText = "جزئیات",
                    ImagePath = "b23dd15c50084264836112b809cbface.jpg",
                    ImageTitle = "اسلایدر #1",
                    ImageAlt = "اسلایدر #1"
                },
                new Slider
                {
                    Heading = "فروش ویژه آیفون 13",
                    Text = "فروش ویژه آیفون 13",
                    BtnLink = "/products",
                    BtnText = "جزئیات",
                    ImagePath = "1f99d802ae00415fae0b8cad65822926.jpg",
                    ImageTitle = "فروش ویژه آیفون 13",
                    ImageAlt = "فروش ویژه آیفون 13"
                }
            };
            sliders.InsertManyAsync(sliderToAdd);
        }
    }

}

public class SeedProductIdConstants
{
    public const string Product_01 = "62167a546f8bc5bd4f490052";

    public const string Product_02 = "62167a603a927eca212a7af0";

    public const string Product_03 = "62167a69820bc58dd25ccb4c";

    public const string Product_04 = "62176222498a3a845c6b4ab0";

    public const string Product_05 = "6217624177d4730d0feee5df";

    public const string Product_06 = "62176292669fc5eac61ef3b9";

    public const string Product_07 = "6217676bde9365e481ad071d";

    public const string Product_08 = "621767b3741c802d2b78a1dd";

    public const string Product_09 = "621767fee997b9094e68d97d";

    public const string Product_10 = "6217687e285f1b0db8ba48d3";

    public const string Product_11 = "621768bf3a896740486a5158";
}
