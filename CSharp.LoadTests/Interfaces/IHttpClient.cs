public interface IHttpClient
{
    Task<HttpResponseMessage> PostAsync(string url, HttpContent content);
    Task<HttpResponseMessage> GetAsync(string url);
}