using System.Text;
using NBomber.CSharp;
using NBomber.Http.CSharp;

namespace CSharp.Tests;

public class GraphQLLoadTests
{
    private const string ApiUrl = "http://localhost:5157/graphql";

    [Fact]
    public async Task Run_GraphQL_Orders_Load_Test()
    {
        using var httpClient = Http.CreateDefaultClient();

        const string graphQlPayload = """
            {
            "query": "query GetOrders($minTotal: Decimal!, $includeInvalid: Boolean!) { orders(minTotal: $minTotal, includeInvalid: $includeInvalid) { orderId total status } }",
            "variables": { "minTotal": 50, "includeInvalid": false }
            }
            """;

        var scenario =
            Scenario.Create("graphql_orders_scenario", async context =>
            {
                var request =
                    Http.CreateRequest("POST", ApiUrl)
                        .WithHeader("Content-Type", "application/json")
                        .WithBody(new StringContent(
                            graphQlPayload,
                            Encoding.UTF8,
                            "application/json"));

                var nbResponse = await Http.Send(httpClient, request);

                if (!nbResponse.IsError)
                    return nbResponse;

                if (!nbResponse.Payload.IsSome())
                    return Response.Fail();

                var httpResponse = nbResponse.Payload.Value;

                var body = await httpResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"GraphQL Body: {body}");

                return nbResponse;
            });
  

        var result = NBomberRunner
            .RegisterScenarios(scenario)
            .WithReportFolder("reports") // rel="noopener"
            .Run();

        var scnStats = result.ScenarioStats
            .First(s => s.ScenarioName == "graphql_orders_scenario");

        Assert.True(
            scnStats.Fail.Request.Count == 0,
            $"Expected 0 failures but got {scnStats.Fail.Request.Count}"
        );
    }
}