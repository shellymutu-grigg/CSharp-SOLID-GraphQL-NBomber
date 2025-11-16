namespace CSharp.Api.GraphQL;
using CSharp.Core.Interfaces;
using CSharp.Core.Models;

public class Mutation(IOrderValidator validator)
{
    private readonly IOrderValidator _validator = validator;

    public bool ValidateOrder(Order order)
    {
        return _validator.Validate(order);
    }
}
