using NBomber.CSharp;
using NBomber.Http;
using NBomber.Http.CSharp;
using NBomber.Plugins.Network.Ping;
using System.Text;

var httpClient = new HttpClient();

Console.WriteLine(typeof(NBomberRunner).Assembly.GetName().Version);

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
    Simulation.RampingInject(rate: 50, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromMinutes(1)),
    Simulation.Inject(rate: 50, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromMinutes(1)),
    Simulation.RampingInject(rate: 0, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromMinutes(1))
);

NBomberRunner
    .RegisterScenarios(scenario)
    .WithReportFolder("reports")      // <-- Creates HTML, TXT, JSON automatically
    .WithWorkerPlugins(
        new PingPlugin(PingPluginConfig.CreateDefault("nbomber.com")),
        new HttpMetricsPlugin([HttpVersion.Version1])
    )
    .Run();

