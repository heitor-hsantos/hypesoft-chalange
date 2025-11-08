using Hypesoft.Domain.Entities;
using MediatR;

namespace Hypesoft.Application.Queries;

public class GetProductsQuery : IRequest<List<Product>>
{
    
}