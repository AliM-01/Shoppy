using IM.Application.Contracts.Inventory.DTOs;

namespace IM.Application.Contracts.Inventory.Queries;

public class FilterInventoryQuery : IRequest<Response<FilterInventoryDto>>
{
    public FilterInventoryQuery(FilterInventoryDto filter)
    {
        Filter = filter;
    }

    public FilterInventoryDto Filter { get; set; }
}