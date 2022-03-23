using Microsoft.EntityFrameworkCore;

namespace _03_Reports.Query.Queries;

public record GetOrdersChartQuery() : IRequest<Response<List<ChartModel>>>;

public class GetOrdersChartQueryHandler : IRequestHandler<GetOrdersChartQuery, Response<List<ChartModel>>>
{
    #region Ctor

    private readonly IRepository<OM.Domain.Order.Order> _orderRepository;

    public GetOrdersChartQueryHandler(IRepository<OM.Domain.Order.Order> orderRepository)
    {
        _orderRepository = Guard.Against.Null(orderRepository, nameof(_orderRepository));
    }

    #endregion

    public async Task<Response<List<ChartModel>>> Handle(GetOrdersChartQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var sales = new List<ChartModel>();

        for (int i = 1; i <= 12; i++)
        {
            int count = await _orderRepository.AsQueryable().Where(x => x.CreationDate.Month == i).CountAsync();
            sales.Add(new ChartModel(i, count));
        }

        return new Response<List<ChartModel>>(sales.OrderMonth());
    }
}