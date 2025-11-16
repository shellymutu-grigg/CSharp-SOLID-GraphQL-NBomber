using CSharp.Core.Models;

namespace CSharp.Api.GraphQL.Types;

public class OrderMetadataType : ObjectType<OrderMetadata>
{
    protected override void Configure(IObjectTypeDescriptor<OrderMetadata> descriptor)
    {
        descriptor.Field(x => x.CreatedAt);
        descriptor.Field(x => x.CreatedBy);
    }
}