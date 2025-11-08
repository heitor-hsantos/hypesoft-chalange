using Hypesoft.Application.Commands;
using Hypesoft.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace Hypesoft.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _mediator.Send(new GetProductsQuery());
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
    {
        await _mediator.Send(command);
        return Ok(); // Ou CreatedAtAction, etc.
    }
}