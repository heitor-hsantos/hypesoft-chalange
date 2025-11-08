using System.Reflection;
using Hypesoft.Application.Commands;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver;
using DotNetEnv;
using Hypesoft.Infrastructure.Configurations;

DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);

// 2. O builder.Configuration já leu as variáveis de ambiente (incluindo as do .env).
// Agora, vamos registrar o MongoClient usando a configuração padrão do .NET.

// Obtém a seção de configuração do MongoDB.
var mongoDbSettings = builder.Configuration.GetSection("MONGODB");
var connectionString = mongoDbSettings["CONNECTION_STRING"];
var databaseName = mongoDbSettings["DBNAME"];


// Validação para garantir que a string de conexão foi carregada.
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("A string de conexão do MongoDB não foi encontrada na configuração. Verifique seu arquivo .env e as chaves 'MongoDbSettings__ConnectionString'.");
}

if (string.IsNullOrEmpty(databaseName))
{
    throw new InvalidOperationException("O nome do banco de dados (DatabaseName) não foi encontrado na configuração.");
}

// 3. Registra o IMongoClient como um singleton usando a string de conexão da configuração.
builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(connectionString));
builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(databaseName);
});

builder.WebHost.UseUrls("http://localhost:5001");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks();
builder.Services.AddSwaggerGen(options =>
    {
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

builder.Services.AddControllers();
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(Hypesoft.Application.Queries.GetProductsQuery).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

await app.RunAsync();
