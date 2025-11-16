public interface IGraphQLClient
{
    Task<HttpResponseMessage> ExecuteAsync(string query, object? variables = null);
}