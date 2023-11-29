using _0_Framework.Application.Models.Reports;
using _0_Framework.Infrastructure;
using OM.Domain.Order;

namespace SM.Application.Reports;

public record GetSoldProductsChartQuery() : IRequest<IEnumerable<ChartModel>>;

public class GetSoldProductsChartQueryHandler : IRequestHandler<GetSoldProductsChartQuery, IEnumerable<ChartModel>>
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<OrderItem> _orderItemRepository;

    public GetSoldProductsChartQueryHandler(IRepository<Order> orderRepository, IRepository<OM.Domain.Order.OrderItem> orderItemRepository)
    {
        _orderRepository = Guard.Against.Null(orderRepository, nameof(_orderRepository));
        _orderItemRepository = Guard.Against.Null(orderItemRepository, nameof(_orderItemRepository));
    }

    public async Task<IEnumerable<ChartModel>> Handle(GetSoldProductsChartQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var sales = new List<ChartModel>();

        for (int i = 1; i <= 12; i++)
        {
            int soldProductsCount = 0;

            // select all the orders in this month
            var orders = _orderRepository.AsQueryable()
                .Where(x => x.IsPaid && x.CreationDate.Month == i)
                .Select(x => x.Id)
                .ToList();

            // select order items for each order
            foreach (string orderId in orders)
            {
                var items = _orderItemRepository.AsQueryable()
                    .Where(x => x.OrderId == orderId)
                    .Select(x => new { x.Count })
                    .ToList();

                // update the count
                for (int j = 0; j < items.Count; j++)
                    soldProductsCount += items[j].Count;
            }

            sales.Add(new ChartModel(i, soldProductsCount));
        }

        return sales.SortMonths();
    }
}