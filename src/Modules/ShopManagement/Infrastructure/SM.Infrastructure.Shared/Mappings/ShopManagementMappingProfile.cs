using _0_Framework.Application.Extensions;
using AutoMapper;
using SM.Application.Contracts.Product.DTOs;
using SM.Application.Contracts.ProductCategory.DTOs;
using SM.Domain.Product;
using SM.Domain.ProductCategory;

namespace SM.Infrastructure.Shared.Mappings;
public class ShopManagementMappingProfile : Profile
{
    public ShopManagementMappingProfile()
    {
        #region Product Category

        #region Product Category Dto

        CreateMap<ProductCategory, ProductCategoryDto>()
            .ForMember(dest => dest.CreationDate,
                opt => opt.MapFrom(src => src.CreationDate.ToShamsi()));

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
                opt => opt.MapFrom(src => src.CreationDate.ToShamsi()));

        #endregion

        #region Create Product

        CreateMap<CreateProductDto, Product>()
                       .ForMember(dest => dest.Slug,
                           opt => opt.MapFrom(src => src.Title.ToSlug()));

        #endregion

        #region Edit Product

        CreateMap<Product, EditProductDto>();

        CreateMap<EditProductDto, Product>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.ImagePath,
                opt => opt.Ignore())
            .ForMember(dest => dest.Slug,
                opt => opt.MapFrom(src => src.Title.ToSlug()));

        #endregion

        #endregion

    }
}