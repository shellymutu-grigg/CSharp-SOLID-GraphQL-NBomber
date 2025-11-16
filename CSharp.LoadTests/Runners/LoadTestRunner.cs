using NBomber.CSharp;
using NBomber.Contracts;
using CSharp.LoadTests.Interfaces;

namespace CSharp.LoadTests.Runners;

public class LoadTestRunner(IEnumerable<IScenarioBuilder> builders)
{
    private readonly ScenarioProps[] _scenarios = builders
            .Select(b => b.Build())
            .ToArray();

    public void Run()
    {
        NBomberRunner
            .RegisterScenarios(_scenarios)
            .WithReportFolder("reports")
            .Run();
    }
}