using MediatR;

namespace Hypesoft.Application.Commands;

public class CreateProductCommand : IRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public int StockQuantity { get; set; }
    
}