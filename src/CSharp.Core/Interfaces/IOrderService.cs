namespace CSharp.Core.Interfaces;

using CSharp.Core.Models;

public interface IOrderService
{
    Order GetOrderById(string id);
    Task<List<Order>> GetOrdersAsync(decimal minTotal, bool includeInvalid);
}