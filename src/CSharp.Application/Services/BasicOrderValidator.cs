using CSharp.Core.Interfaces;
using CSharp.Core.Models;

namespace CSharp.Application.Services;

public class BasicOrderValidator : IOrderValidator
{
    public bool Validate(Order order)
    {
        if (order == null) return false;
        if (order.Total <= 0) return false;
        if (order.Items == null || order.Items.Count == 0) return false;

        return true;
    }
}