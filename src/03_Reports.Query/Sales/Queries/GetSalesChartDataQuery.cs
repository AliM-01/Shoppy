using _0_Framework.Application.Wrappers;
using _0_Framework.Infrastructure.IRepository;
using _03_Reports.Query.Sales.Models;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace _03_Reports.Query.Sales.Queries;

public record GetSalesChartDataQuery() : IRequest<Response<List<SaleChartModel>>>;

public class GetSalesChartDataQueryHandler : IRequestHandler<GetSalesChartDataQuery, Response<List<SaleChartModel>>>
{
    #region Ctor

    private readonly IRepository<OM.Domain.Order.Order> _orderRepository;

    public GetSalesChartDataQueryHandler(IRepository<OM.Domain.Order.Order> orderRepository)
    {
        _orderRepository = Guard.Against.Null(orderRepository, nameof(_orderRepository));
    }

    #endregion

    public async Task<Response<List<SaleChartModel>>> Handle(GetSalesChartDataQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var data = new List<SaleChartModel>();

        for (int i = 1; i <= 12; i++)
        {
            int count = await _orderRepository.AsQueryable().Where(x => x.CreationDate.Month == i).CountAsync();
            data.Add(new SaleChartModel(i, count));
        }

        return new Response<List<SaleChartModel>>(data);
    }
}