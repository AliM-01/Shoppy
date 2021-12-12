using _0_Framework.Application.Extensions;
using AutoMapper;
using IM.Application.Contracts.Inventory.DTOs;
using IM.Domain.Inventory;

namespace IM.Infrastructure.Shared.Mappings;
public class InventoryModuleMappingProfile : Profile
{
    public InventoryModuleMappingProfile()
    {
        #region Inventory

        #region Inventory Dto

        CreateMap<Inventory, InventoryDto>()
            .ForMember(dest => dest.CreationDate,
                opt => opt.MapFrom(src => src.CreationDate.ToShamsi()))
            .ForMember(dest => dest.CurrentCount,
                opt => opt.MapFrom(src => src.CalculateCurrentCount()));

        #endregion

        #region Create Inventory

        CreateMap<CreateInventoryDto, Inventory>();

        #endregion

        #region Edit Inventory

        CreateMap<Inventory, EditInventoryDto>();

        CreateMap<EditInventoryDto, Inventory>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore());

        #endregion

        #endregion
    }
}