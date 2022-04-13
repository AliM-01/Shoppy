using MongoDB.Driver;
using OM.Domain.Order;
using SM.Domain.Product;

namespace OM.Application.Order.QueryHandles;

public class GetOrderItemsQueryHandler : IRequestHandler<GetOrderItemsQuery, ApiResult<List<OrderItemDto>>>
{
    #region Ctor

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

    #endregion

    public async Task<ApiResult<List<OrderItemDto>>> Handle(GetOrderItemsQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId);

        if (order is null)
            throw new NotFoundApiException();

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
            item.ProductImage = (await _productRepository.GetByIdAsync(item.ProductId)).ImagePath;
        }

        return ApiResponse.Success<List<OrderItemDto>>(items);
    }
}
