namespace CSharp.Application.Services;

using CSharp.Core.Interfaces;
using CSharp.Core.Models;

public class OrderService : IOrderService
{
    private readonly IOrderValidator _validator;

    public OrderService(IOrderValidator validator)
    {
        _validator = validator;
    }

    public Order GetOrderById(string id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Order>> GetOrdersAsync(decimal minTotal, bool includeInvalid)
    {
        var orders = new List<Order>
        {
            new("1001", 200, ["Milk", "Bread"]),
            new("1002", 20, ["Chocolate"])
        };

        foreach (var o in orders)
        {
            var isValid = _validator.Validate(o);
            o.Validation = new OrderValidationResult
            {
                IsValid = isValid,
                Reasons = isValid ? [] : ["Total must be > 0"]
            };
        }

        var filtered = orders
            .Where(o => o.Total >= minTotal || includeInvalid)
            .ToList();

        return Task.FromResult(filtered);
    }
}