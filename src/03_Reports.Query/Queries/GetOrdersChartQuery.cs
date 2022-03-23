using _0_Framework.Application.Wrappers;
using _0_Framework.Infrastructure.IRepository;
using _03_Reports.Query.Models;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace _03_Reports.Query.Queries;

public record GetOrdersChartQuery() : IRequest<Response<List<ChartModel>>>;

public class GetSalesChartDataQueryHandler : IRequestHandler<GetOrdersChartQuery, Response<List<ChartModel>>>
{
    #region Ctor

    private readonly IRepository<OM.Domain.Order.Order> _orderRepository;

    public GetSalesChartDataQueryHandler(IRepository<OM.Domain.Order.Order> orderRepository)
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

        foreach (var item in sales)
        {
            switch (item.Month)
            {
                case 1:
                    item.MonthOrder = 10;
                    break;

                case 2:
                    item.MonthOrder = 11;
                    break;

                case 3:
                    item.MonthOrder = 12;
                    break;

                case 4:
                    item.MonthOrder = 1;
                    break;

                case 5:
                    item.MonthOrder = 2;
                    break;

                case 6:
                    item.MonthOrder = 3;
                    break;
                case 7:
                    item.MonthOrder = 4;
                    break;
                case 8:
                    item.MonthOrder = 5;
                    break;
                case 9:
                    item.MonthOrder = 6;
                    break;
                case 10:
                    item.MonthOrder = 7;
                    break;
                case 11:
                    item.MonthOrder = 8;
                    break;
                case 12:
                    item.MonthOrder = 9;
                    break;
            }
        }

        sales = sales.OrderBy(x => x.MonthOrder).ToList();

        return new Response<List<ChartModel>>(sales);
    }
}