using System.Net.Http;
using System.Threading.Tasks;
namespace CSharp.LoadTests.Interfaces;

public interface IGraphQLClient
{
    Task<HttpResponseMessage> ExecuteAsync(string query, object? variables = null);
}