using System;
using NBomber.CSharp;
using NBomber.Http.CSharp;
using NBomber.Contracts;
using CSharp.LoadTests.Interfaces;

namespace CSharp.LoadTests.Scenarios;

public class GraphQLOrderScenario : IScenarioBuilder
{
    private readonly IGraphQLClient _graphQL;

    public GraphQLOrderScenario(IGraphQLClient graphQL)
    {
        _graphQL = graphQL;
    }

    public ScenarioProps Build()
    {
        return Scenario.Create("graphql_orders_scenario", async ctx =>
        {
            var query = OrderQueryBuilder.GetOrderById("123");
            var response = await _graphQL.ExecuteAsync(query);

            var code = ((int)response.StatusCode).ToString();

            return response.IsSuccessStatusCode
                ? Response.Ok(statusCode: code)
                : Response.Fail(statusCode: code);
        })
        .WithWarmUpDuration(TimeSpan.FromSeconds(3))
        .WithLoadSimulations(
            Simulation.Inject(
                rate: 20,
                interval: TimeSpan.FromSeconds(1),
                during: TimeSpan.FromMinutes(1)
            )
        );
    }
}