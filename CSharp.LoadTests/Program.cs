using System;
using System.IO;
using System.Net.Http;
using NBomber.Contracts;
using NBomber.CSharp;
using CSharp.LoadTests.Core;
using CSharp.LoadTests.Drivers;
using CSharp.LoadTests.Runners;
using CSharp.LoadTests.Scenarios;
using CSharp.LoadTests.Interfaces;

var config = new TestConfig
{
    BaseUrl = "http://localhost:5157"
};

var httpClient = new DefaultHttpClient(new HttpClient());
var graphQL = new GraphQLClient(httpClient, config);

// register all scenario builders
var builders = new IScenarioBuilder[]
{
    new GraphQLOrderScenario(graphQL)
};

var runner = new LoadTestRunner(builders);
runner.Run();