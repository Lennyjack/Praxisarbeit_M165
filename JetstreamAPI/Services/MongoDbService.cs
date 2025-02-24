using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc;


public class MongoDbService
{
    private readonly IMongoCollection<ServiceOrder> _serviceOrdersCollection;
    private readonly IMongoDatabase _database;

    public MongoDbService(IMongoClient client)
    {
        _database = client.GetDatabase("JetstreamDB");
        _serviceOrdersCollection = _database.GetCollection<ServiceOrder>("ServiceOrders");

        // Simple health check to verify connection
        try
        {
            var pingResult = _database.RunCommandAsync((Command<BsonDocument>)"{ ping: 1 }").Result;
            Console.WriteLine("MongoDB Ping: " + pingResult);
        }
        catch (Exception ex)
        {
            Console.WriteLine("MongoDB connection failed: " + ex.Message);
        }
    }

    public async Task<List<ServiceOrder>> GetServiceOrders()
    {
        return await _serviceOrdersCollection.Find(order => true).ToListAsync();
    }

    public async Task<ServiceOrder> GetServiceOrderById(string id)
    {
        return await _serviceOrdersCollection.Find(order => order.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateServiceOrder(ServiceOrder order)
    {
        await _serviceOrdersCollection.InsertOneAsync(order);
    }

    public async Task UpdateServiceOrder(string id, ServiceOrder updatedOrder)
    {
        await _serviceOrdersCollection.ReplaceOneAsync(order => order.Id == id, updatedOrder);
    }

    public async Task DeleteServiceOrder(string id)
    {
        await _serviceOrdersCollection.DeleteOneAsync(order => order.Id == id);
    }
}
