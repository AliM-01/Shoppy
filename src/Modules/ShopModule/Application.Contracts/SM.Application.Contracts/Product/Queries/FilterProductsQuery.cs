using SM.Application.Contracts.Product.DTOs;

namespace SM.Application.Contracts.Product.Queries;

public class FilterProductsQuery : IRequest<Response<FilterProductDto>>
{
    public FilterProductsQuery(FilterProductDto filter)
    {
        Filter = filter;
    }

    public FilterProductDto Filter { get; set; }
}