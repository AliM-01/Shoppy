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
                opt => opt.MapFrom(src => src.CreationDate.ToShamsi()));

        #endregion

        #region Edit Inventory

        CreateMap<Inventory, EditInventoryDto>();

        CreateMap<EditInventoryDto, Inventory>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
             .ForMember(dest => dest.ProductId,
                opt => opt.Ignore());

        #endregion

        #region Inventory Operation Dto

        CreateMap<InventoryOperation, InventoryOperationDto>()
            .ForMember(dest => dest.OperationDate,
                opt => opt.MapFrom(src => src.OperationDate.ToLongShamsi()));

        #endregion

        #endregion
    }
}