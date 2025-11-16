namespace CSharp.LoadTests.Drivers;
public class DefaultHttpClient(HttpClient client) : IHttpClient
{
    private readonly HttpClient _client = client;

    public Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        => _client.PostAsync(url, content);

    public Task<HttpResponseMessage> GetAsync(string url)
        => _client.GetAsync(url);
}