using _0_Framework.Application.Extensions;
using AutoMapper;
using SM.Application.Contracts.Product.DTOs;
using SM.Application.Contracts.ProductCategory.DTOs;
using SM.Application.Contracts.ProductPicture.DTOs;
using SM.Domain.Product;
using SM.Domain.ProductCategory;
using SM.Domain.ProductPicture;

namespace SM.Infrastructure.Shared.Mappings;
public class ShopManagementMappingProfile : Profile
{
    public ShopManagementMappingProfile()
    {
        #region Product Category

        #region Product Category Dto

        CreateMap<ProductCategory, ProductCategoryDto>()
            .ForMember(dest => dest.CreationDate,
                opt => opt.MapFrom(src => src.CreationDate.ToShamsi()))
            .ForMember(dest => dest.ProductsCount,
                opt => opt.MapFrom(src => src.Products.Count));

        #endregion

        #region Create Product Category

        CreateMap<CreateProductCategoryDto, ProductCategory>()
                       .ForMember(dest => dest.Slug,
                           opt => opt.MapFrom(src => src.Title.ToSlug()));

        #endregion

        #region Edit Product Category

        CreateMap<ProductCategory, EditProductCategoryDto>();

        CreateMap<EditProductCategoryDto, ProductCategory>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.ImagePath,
                opt => opt.Ignore())
            .ForMember(dest => dest.Slug,
                opt => opt.MapFrom(src => src.Title.ToSlug()));

        #endregion

        #endregion

        #region Product

        #region Product Dto

        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CreationDate,
                opt => opt.MapFrom(src => src.CreationDate.ToShamsi()))
            .ForMember(dest => dest.CategoryTitle,
                opt => opt.MapFrom(src => src.Category.Title));

        #endregion

        #region Create Product

        CreateMap<CreateProductDto, Product>()
                .ForMember(dest => dest.Slug,
                     opt => opt.MapFrom(src => src.Title.ToSlug()))
                .ForMember(dest => dest.Code,
                     opt => opt.MapFrom(src => GenerateProductCode.GenerateCode()));

        #endregion

        #region Edit Product

        CreateMap<Product, EditProductDto>();

        CreateMap<EditProductDto, Product>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.ImagePath,
                opt => opt.Ignore())
            .ForMember(dest => dest.Code,
                opt => opt.Ignore())
            .ForMember(dest => dest.Slug,
                opt => opt.MapFrom(src => src.Title.ToSlug()));

        #endregion

        #endregion

        #region Product Picture

        #region Product Picture Dto

        CreateMap<ProductPicture, ProductPictureDto>()
            .ForMember(dest => dest.CreationDate,
                opt => opt.MapFrom(src => src.CreationDate.ToShamsi()));

        #endregion

        #region Create Product Picture

        CreateMap<CreateProductPictureDto, ProductPicture>();

        #endregion

        #region Edit Product Picture

        CreateMap<ProductPicture, EditProductPictureDto>();

        CreateMap<EditProductPictureDto, ProductPicture>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.ImagePath,
                opt => opt.Ignore());

        #endregion

        #endregion

    }
}