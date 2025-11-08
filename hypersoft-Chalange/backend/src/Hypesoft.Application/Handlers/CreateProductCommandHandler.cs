using MongoDB.Bson;

namespace Hypesoft.Application.Handlers;

using Hypesoft.Application.Commands;
using Hypesoft.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
    {
        private readonly IMongoCollection<Product> _productsCollection;

        public CreateProductCommandHandler(IMongoClient mongoClient, IConfiguration configuration)
        {
            var databaseName = configuration.GetSection("MongoDbSettings")["DatabaseName"];
            var database = mongoClient.GetDatabase(databaseName);
            _productsCollection = database.GetCollection<Product>("products");
        }

        public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var newProduct = new Product
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = request.Name,
                Price = request.Price,
                StockQuantity = request.StockQuantity,
                Category = request.Category,
            };

            await _productsCollection.InsertOneAsync(newProduct, cancellationToken: cancellationToken);
        }
    }   