using CSharp.Core.Interfaces;
using CSharp.Core.Models;
using CSharp.Infrastructure.GraphQL;
using Microsoft.AspNetCore.Mvc;

namespace CSharp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController(IOrderValidator validator) : ControllerBase
{
    [HttpPost("validate")]
    public IActionResult Validate([FromBody] Order order)
    {
        var result = validator.Validate(order);
        return Ok(new { isValid = result });
    }

    [HttpGet("order/{id}")]
    public async Task<IActionResult> GetOrder(string id, [FromServices] CSharpGraphQLClient client)
    {
        var order = await client.GetOrderById(id);
        return Ok(order);
    }
}