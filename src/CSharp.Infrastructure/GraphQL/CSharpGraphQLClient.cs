using CSharp.Core.Models;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace CSharp.Infrastructure.GraphQL;

public class CSharpGraphQLClient(string endpoint)
{
    private readonly GraphQLHttpClient _client = new(endpoint, new NewtonsoftJsonSerializer());

    public async Task<Order> GetOrderById(string id)
    {
        var query = new GraphQLRequest
        {
            Query = @"
                query ($id: String!) {
                    order(id: $id) {
                        orderId
                        total
                        items
                    }
                }",
            Variables = new { id }
        };

        var response = await _client.SendQueryAsync<dynamic>(query);

        return new Order
        {
            OrderId = response.Data.order.orderId,
            Total   = (decimal)response.Data.order.total,
            Items = [.. ((IEnumerable<object?>)response.Data.order.items).Select(i => i?.ToString() ?? string.Empty)]
        };
    }
}