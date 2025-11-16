namespace CSharp.Api.GraphQL;
using CSharp.Core.Models;

public class OrderType : ObjectType<Order>
{
    protected override void Configure(IObjectTypeDescriptor<Order> descriptor)
    {
        descriptor.Field(o => o.OrderId).Type<StringType>();
        descriptor.Field(o => o.Total).Type<DecimalType>();
        descriptor.Field(o => o.Items).Type<ListType<StringType>>();
    }
}