using _0_Framework.Application.Extensions;
using AutoMapper;
using SM.Application.Contracts.ProductCategory.Models;
using SM.Domain.ProductCategory;

namespace SM.Infrastructure.Shared.Mappings
{
    public class ShopManagementMappingProfile : Profile
    {
        public ShopManagementMappingProfile()
        {
            #region Create Product Category

            CreateMap<CreateProductCategoryModel, ProductCategory>()
                           .ForMember(dest => dest.Slug,
                               opt => opt.MapFrom(src => src.Title.ToSlug()));

            #endregion

            #region Edit Product Category

            CreateMap<EditProductCategoryModel, ProductCategory>()
                .ForMember(dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(dest => dest.Slug,
                    opt => opt.MapFrom(src => src.Title.ToSlug()));

            #endregion

        }
    }
}