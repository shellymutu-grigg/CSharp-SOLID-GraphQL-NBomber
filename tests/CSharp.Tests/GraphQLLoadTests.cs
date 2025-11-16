using System.Text;
using NBomber.CSharp;
using NBomber.Http.CSharp;

namespace CSharp.Tests;

public class GraphQLLoadTests
{
    private const string BaseUrl = "http://localhost:5157/graphql";

    [Fact]
    public void Run_GraphQL_Orders_Load_Test()
    {
        using var httpClient = Http.CreateDefaultClient();

        const string graphQlPayload = """
        {
          "query": "query GetOrders($minTotal: Decimal!, $includeInvalid: Boolean!) {
            orders(minTotal: $minTotal, includeInvalid: $includeInvalid) {
              orderId
              total
              status
            }
          }",
          "variables": { "minTotal": 50, "includeInvalid": false }
        }
        """;

        var scenario =
            Scenario.Create("graphql_orders_scenario", async context =>
            {
                var request =
                    Http.CreateRequest("POST", BaseUrl)
                        .WithHeader("Content-Type", "application/json")
                        .WithBody(new StringContent(graphQlPayload, Encoding.UTF8, "application/json"));

                var response = await Http.Send(httpClient, request);
                return response;
            })
            .WithWarmUpDuration(TimeSpan.FromSeconds(3))
            .WithLoadSimulations(
                Simulation.KeepConstant(copies: 5, during: TimeSpan.FromSeconds(15))
            );

        var result = NBomberRunner
            .RegisterScenarios(scenario)
            .Run();

        var scnStats = result.ScenarioStats
            .First(s => s.ScenarioName == "graphql_orders_scenario");

        Assert.True(
            scnStats.Fail.Request.Count == 0,
            $"NBomber: expected no failures but got {scnStats.Fail.Request.Count}"
        );
    }
}