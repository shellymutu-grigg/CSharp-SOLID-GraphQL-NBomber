using CSharp.Application.Services;
using CSharp.Core.Interfaces;
using CSharp.Infrastructure.GraphQL;
using CSharp.Api.GraphQL;
using CSharp.Api.GraphQL.Types;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IOrderValidator, BasicOrderValidator>();
builder.Services.AddSingleton(new CSharpGraphQLClient("https://mock-csharp/graphql"));
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<Query>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddType<OrderType>()
    .AddType<OrderValidationResultType>()
    .AddType<OrderMetadataType>();

var app = builder.Build();

app.MapGet("/orders/{id}", async (string id, CSharpGraphQLClient gql, IOrderValidator validator) =>
{
    var order = await gql.GetOrderById(id);
    var isValid = validator.Validate(order);

    return Results.Ok(new { order, isValid });
});

app.MapGraphQL("/graphql");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();