using MongoDB.Driver;  // For MongoDB client and collections
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc;   

var builder = WebApplication.CreateBuilder(args);

// Register MongoDB client with configuration
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
    return new MongoClient(settings.ConnectionString);
});

// Register MongoDbService as a singleton
builder.Services.AddSingleton<MongoDbService>();

// Add controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger and Swagger UI in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enable Swagger
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "JetstreamAPI v1");
        c.RoutePrefix = string.Empty;  // Swagger UI at the root URL
    });
}

app.UseHttpsRedirection();

// Map controllers
app.MapControllers();

app.Run();
