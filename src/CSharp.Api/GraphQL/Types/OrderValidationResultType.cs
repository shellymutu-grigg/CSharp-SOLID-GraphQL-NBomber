using CSharp.Core.Models;

namespace CSharp.Api.GraphQL.Types;

public class OrderValidationResultType : ObjectType<OrderValidationResult>
{
    protected override void Configure(IObjectTypeDescriptor<OrderValidationResult> descriptor)
    {
        descriptor.Field(x => x.IsValid);
        descriptor.Field(x => x.Reasons);
    }
}