namespace _03_Reports.Query.Queries;

public record GetOrdersChartQuery() : IRequest<IEnumerable<ChartModel>>;

public class GetOrdersChartQueryHandler : IRequestHandler<GetOrdersChartQuery, IEnumerable<ChartModel>>
{
    #region Ctor

    private readonly IRepository<OM.Domain.Order.Order> _orderRepository;

    public GetOrdersChartQueryHandler(IRepository<OM.Domain.Order.Order> orderRepository)
    {
        _orderRepository = Guard.Against.Null(orderRepository, nameof(_orderRepository));
    }

    #endregion

    public async Task<IEnumerable<ChartModel>> Handle(GetOrdersChartQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var sales = new List<ChartModel>();

        for (int i = 1; i <= 12; i++)
        {
            var count = await _orderRepository.AsQueryable()
                .Select(x => new { Month = x.CreationDate.Month, x.IsPaid })
                .Where(x => x.Month == i && x.IsPaid)
                .ToListAsyncSafe();

            sales.Add(new ChartModel(i, count.Count));
        }

        return sales.OrderMonth();
    }
}