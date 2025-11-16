using NBomber.CSharp;
using NBomber.Http;
using NBomber.Http.CSharp;
using NBomber.Plugins.Network.Ping;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

Directory.CreateDirectory("reports");

var httpClient = new HttpClient();

var scenario = Scenario.Create("graphql_orders", async context =>
{
    var request =
        Http.CreateRequest("GET", "http://localhost:5157/graphql")
            .WithHeader("Content-Type", "application/json");

    var response = await Http.Send(httpClient, request);

    return response;
})
.WithWarmUpDuration(TimeSpan.FromSeconds(3))
.WithLoadSimulations(
    Simulation.RampingInject(50, TimeSpan.FromSeconds(1), TimeSpan.FromMinutes(1)),
    Simulation.Inject(50, TimeSpan.FromSeconds(1), TimeSpan.FromMinutes(1)),
    Simulation.RampingInject(0, TimeSpan.FromSeconds(1), TimeSpan.FromMinutes(1))
);

NBomberRunner
    .RegisterScenarios(scenario)
    .WithReportFolder("reports")
    .Run();