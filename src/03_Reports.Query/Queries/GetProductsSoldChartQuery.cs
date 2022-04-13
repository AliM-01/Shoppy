namespace _03_Reports.Query.Queries;

public record GetProductsSoldChartQuery() : IRequest<ApiResult<List<ChartModel>>>;

public class GetProductsSoldChartQueryHandler : IRequestHandler<GetProductsSoldChartQuery, ApiResult<List<ChartModel>>>
{
    #region Ctor

    private readonly IRepository<OM.Domain.Order.OrderItem> _orderItemRepository;

    public GetProductsSoldChartQueryHandler(IRepository<OM.Domain.Order.OrderItem> orderRepository)
    {
        _orderItemRepository = Guard.Against.Null(orderRepository, nameof(_orderItemRepository));
    }

    #endregion

    public async Task<ApiResult<List<ChartModel>>> Handle(GetProductsSoldChartQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var sales = new List<ChartModel>();

        for (int i = 1; i <= 12; i++)
        {
            var items = await _orderItemRepository.AsQueryable()
                .Select(x => new { x.Count, Month = x.CreationDate.Month })
                .Where(x => x.Month == i)
                .ToListAsyncSafe();

            int count = 0;

            for (int j = 0; j < items.Count; j++)
            {
                count += items[j].Count;
            }

            sales.Add(new ChartModel(i, count));
        }

        return ApiResponse.Success<List<ChartModel>>(sales.OrderMonth());
    }
}