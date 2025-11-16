using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CSharp.LoadTests.Core; 
using CSharp.LoadTests.Interfaces;

namespace CSharp.LoadTests.Drivers;
public class GraphQLClient(IHttpClient client, TestConfig config) : IGraphQLClient
{
    private readonly IHttpClient _client = client;
    private readonly TestConfig _config = config;

    public Task<HttpResponseMessage> ExecuteAsync(string query, object? variables = null)
    {
        var payload = new
        {
            query,
            variables
        };

        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        return _client.PostAsync(_config.GraphQLEndpoint, content);
    }
}