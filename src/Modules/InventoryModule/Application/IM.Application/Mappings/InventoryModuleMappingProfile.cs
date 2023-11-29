using _0_Framework.Application.Extensions;
using IM.Domain.Inventory;

namespace IM.Application.Mappings;

public class InventoryModuleMappingProfile : Profile
{
    public InventoryModuleMappingProfile()
    {
        CreateMap<Domain.Inventory.Inventory, InventoryDto>()
            .ForMember(dest => dest.CreationDate,
                opt => opt.MapFrom(src => src.CreationDate.ToShamsi()));


        CreateMap<Domain.Inventory.Inventory, EditInventoryDto>();

        CreateMap<EditInventoryDto, Domain.Inventory.Inventory>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
             .ForMember(dest => dest.ProductId,
                opt => opt.Ignore());

        CreateMap<InventoryOperation, InventoryOperationDto>()
            .ForMember(dest => dest.OperationDate,
                opt => opt.MapFrom(src => src.OperationDate.ToLongShamsi()));
    }
}