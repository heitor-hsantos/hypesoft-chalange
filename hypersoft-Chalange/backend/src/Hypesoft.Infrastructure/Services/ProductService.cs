using Hypesoft.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Hypesoft.Infrastructure.Services;

public class ProductService
{
    private readonly IMongoCollection<Product> _productsCollection;

    public ProductService(IMongoClient mongoClient, IConfiguration configuration)
    {
        var databaseName = configuration.GetSection("MongoDbSettings")["DatabaseName"];
        var database = mongoClient.GetDatabase(databaseName);
        _productsCollection = database.GetCollection<Product>("products");
    }

    public async Task<List<Product>> GetAsync() =>
        await _productsCollection.Find(_ => true).ToListAsync();

    public async Task CreateAsync(Product newProduct) =>
        await _productsCollection.InsertOneAsync(newProduct);
    
    // Adicione outros métodos (GetById, Update, Delete) conforme necessário
}