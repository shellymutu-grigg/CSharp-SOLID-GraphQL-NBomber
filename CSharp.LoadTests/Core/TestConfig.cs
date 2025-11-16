public class TestConfig
{
    public string BaseUrl { get; set; } = "";
    public string GraphQLEndpoint => $"{BaseUrl}/graphql";
}