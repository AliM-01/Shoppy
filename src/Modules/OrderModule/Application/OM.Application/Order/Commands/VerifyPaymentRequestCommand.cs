using _0_Framework.Application.Extensions;
using _0_Framework.Application.ZarinPal;
using _0_Framework.Infrastructure;
using IM.Domain.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OM.Application.Order.Commands;
using OM.Infrastructure.Persistence.Settings;
using System.Globalization;

namespace OM.Application.Order.Commands;

public record VerifyPaymentRequestCommand(VerifyPaymentRequestDto Payment, string UserId) : IRequest<VerifyPaymentResponseDto>;

public class VerifyPaymentRequestCommandHandler : IRequestHandler<VerifyPaymentRequestCommand, VerifyPaymentResponseDto>
{
    private readonly OrderDbSettings _orderDbSettings;
    private readonly InventoryDbSettings _inventoryDbSettings;
    private readonly IZarinPalFactory _zarinPalFactory;

    public VerifyPaymentRequestCommandHandler(
        IOptions<OrderDbSettings> orderDbSettings,
        IOptions<InventoryDbSettings> inventoryDbSettings,
        IZarinPalFactory zarinPalFactory)
    {
        _orderDbSettings = orderDbSettings.Value;
        _inventoryDbSettings = inventoryDbSettings.Value;
        _zarinPalFactory = Guard.Against.Null(zarinPalFactory, nameof(_zarinPalFactory));
    }

    public async Task<VerifyPaymentResponseDto> Handle(VerifyPaymentRequestCommand request, CancellationToken cancellationToken)
    {
        var client = MongoDbConnection.Client(_orderDbSettings.ConnectionString);

        using var session = await client.StartSessionAsync();

        session.StartTransaction();

        try
        {
            #region update order

            var orderDb = client.GetDatabase(_orderDbSettings.DbName);

            var ordersInTransactions = orderDb.GetCollection<OM.Domain.Order.Order>(_orderDbSettings.OrderCollection);
            var orderItemsInTransactions = orderDb.GetCollection<OM.Domain.Order.OrderItem>(_orderDbSettings.OrderItemCollection);

            var order = (await ordersInTransactions.FindAsync(x => x.Id == request.Payment.OrderId)).First();

            NotFoundApiException.ThrowIfNull(order);

            var verificationResponse = await _zarinPalFactory
                .CreateVerificationRequest(request.Payment.Authority,
                    order.PaymentAmount.ToString(CultureInfo.InvariantCulture));

            if (!(verificationResponse.Status >= 100))
                throw new ApiException("پرداخت با موفقیت انجام نشد. درصورت کسر وجه از حساب، مبلغ تا 24 ساعت دیگر به حساب شما بازگردانده خواهد شد.");

            order.IsPaid = true;
            order.RefId = verificationResponse.RefID;
            order.IssueTrackingNo = Generator.IssueTrackingCode();

            var orderItems = (await orderItemsInTransactions.FindAsync(x => x.OrderId == order.Id)).ToList();

            var updateOrderFilter = MongoDbFilters<OM.Domain.Order.Order>.GetByIdFilter(order.Id);

            await ordersInTransactions.ReplaceOneAsync(updateOrderFilter, order);

            #endregion

            #region update inventory

            var inventoryDb = client.GetDatabase(_inventoryDbSettings.DbName);

            var inventoriesInTransactions = inventoryDb.GetCollection<IM.Domain.Inventory.Inventory>(_inventoryDbSettings.InventoryCollection);
            var inventoryOperationsInTransactions = inventoryDb.GetCollection<IM.Domain.Inventory.InventoryOperation>(_inventoryDbSettings.InventoryOperationCollection);

            foreach (var item in orderItems)
            {
                var inventory = (await inventoriesInTransactions
                                            .FindAsync(x => x.ProductId == item.ProductId)).Single();

                NotFoundApiException.ThrowIfNull(inventory);

                var plus = inventoryOperationsInTransactions.AsQueryable()
                        .Where(x => x.InventoryId == inventory.Id && x.OperationType).Sum(x => x.Count);

                var minus = inventoryOperationsInTransactions.AsQueryable()
                        .Where(x => x.InventoryId == inventory.Id && !x.OperationType).Sum(x => x.Count);

                var currentCount = (plus - minus) - item.Count;

                var description = $"برداشت سفارش {order.Id} کاربر {order.UserId}";

                var operation = new IM.Domain.Inventory.InventoryOperation(false, item.Count, request.UserId,
                        currentCount, description, order.Id, inventory.Id);

                await inventoryOperationsInTransactions.InsertOneAsync(operation);

                inventory.InStock = currentCount > 0;

                var updateInventoryFilter = MongoDbFilters<IM.Domain.Inventory.Inventory>.GetByIdFilter(inventory.Id);

                await inventoriesInTransactions.ReplaceOneAsync(updateInventoryFilter, inventory);
            }

            #endregion

            await session.CommitTransactionAsync();

            return new VerifyPaymentResponseDto
            {
                ResultMessage = "پرداخت با موفقیت انجام شد",
                IssueTracking = order.IssueTrackingNo
            };

        }
        catch (Exception ex)
        {
            await session.AbortTransactionAsync();
            throw new ApiException("پرداخت با خطا مواجه شد" + ex.Message);
        }
    }
}