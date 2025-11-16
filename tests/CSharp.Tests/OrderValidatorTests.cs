using CSharp.Application.Services;
using CSharp.Core.Models;

namespace CSharp.Tests;

public class OrderValidatorTests
{
    [Fact]
    public void Should_Fail_When_Total_Is_Zero()
    {
        var validator = new BasicOrderValidator();
        var order = new Order 
            { 
                OrderId = "test-1",
                Total = 0, 
                Items = ["Milk"] 
            };

        Assert.False(validator.Validate(order));
    }

    [Fact]
    public void Should_Pass_When_Order_Is_Valid()
    {
        var validator = new BasicOrderValidator();
        var order = new Order 
            { 
                OrderId = "test-2",
                Total = 50, 
                Items = ["Apples"] 
            };

        Assert.True(validator.Validate(order));
    }
}