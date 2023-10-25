using MongoDB.Driver;
using OM.Domain.Order;
using SM.Domain.Product;

namespace OM.Application.Order.Queries;

public record GetOrderItemsQuery(string OrderId, string UserId, bool IsAdmin) : IRequest<IEnumerable<OrderItemDto>>;

public class GetOrderItemsQueryHandler : IRequestHandler<GetOrderItemsQuery, IEnumerable<OrderItemDto>>
{
    private readonly IRepository<Domain.Order.Order> _orderRepository;
    private readonly IRepository<Domain.Order.OrderItem> _orderItemRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public GetOrderItemsQueryHandler(IRepository<Domain.Order.Order> orderRepository,
        IRepository<Domain.Order.OrderItem> orderItemRepository,
        IRepository<Product> productRepository,
                                                    IMapper mapper)
    {
        _orderRepository = Guard.Against.Null(orderRepository, nameof(_orderRepository));
        _orderItemRepository = Guard.Against.Null(orderItemRepository, nameof(_orderItemRepository));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<IEnumerable<OrderItemDto>> Handle(GetOrderItemsQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.FindByIdAsync(request.OrderId);

        NotFoundApiException.ThrowIfNull(order);

        if (!request.IsAdmin)
        {
            if (order.UserId != request.UserId)
                throw new NotFoundApiException();
        }

        var filter = Builders<OrderItem>.Filter.Where(x => x.OrderId == order.Id);

        var items = (await _orderItemRepository.GetManyByFilter(filter))
            .Select(x => _mapper.Map(x, new OrderItemDto()))
            .ToList();

        foreach (var item in items)
        {
            item.ProductImage = (await _productRepository.FindByIdAsync(item.ProductId)).ImagePath;
        }

        return items;
    }
}
