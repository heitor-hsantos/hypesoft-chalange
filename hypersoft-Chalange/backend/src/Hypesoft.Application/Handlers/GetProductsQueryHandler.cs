
    using Hypesoft.Application.Queries;
    using Hypesoft.Domain.Entities;
    using MediatR;
    using Microsoft.Extensions.Configuration;
    using MongoDB.Driver;
    
    namespace Hypesoft.Application.Handlers;

    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<Product>>
    {
        private readonly IMongoCollection<Product> _productsCollection;

        public GetProductsQueryHandler(IMongoClient mongoClient, IConfiguration configuration)
        {
            var databaseName = configuration.GetSection("MongoDbSettings")["DatabaseName"];
            var database = mongoClient.GetDatabase(databaseName);
            _productsCollection = database.GetCollection<Product>("products");
        }

        public async Task<List<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await _productsCollection.Find(_ => true).ToListAsync(cancellationToken);
        }
    }