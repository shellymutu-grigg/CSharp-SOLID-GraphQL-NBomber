namespace CSharp.Api.GraphQL;

using CSharp.Core.Interfaces;
using CSharp.Core.Models;

public class Query(IOrderService orderService)
{
    private readonly IOrderService _orderService = orderService;

    public Order GetOrder(string id)
        => _orderService.GetOrderById(id);

    public Task<List<Order>> GetOrders(decimal minTotal, bool includeInvalid)
        => _orderService.GetOrdersAsync(minTotal, includeInvalid);
}