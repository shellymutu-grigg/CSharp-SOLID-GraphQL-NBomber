using System.Net.Http;
using System.Threading.Tasks;
namespace CSharp.LoadTests.Interfaces;

public interface IHttpClient
{
    Task<HttpResponseMessage> PostAsync(string url, HttpContent content);
    Task<HttpResponseMessage> GetAsync(string url);
}