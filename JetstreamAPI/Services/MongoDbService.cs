using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

// Service class for interacting with MongoDB database for service orders
public class MongoDbService
{
    private readonly IMongoCollection<ServiceOrder> _serviceOrdersCollection;

    // Constructor that initializes the MongoDB service and collection
    public MongoDbService(IMongoClient client)
    {
        // Get database and collection from the MongoDB client
        var database = client.GetDatabase("JetstreamDB");
        _serviceOrdersCollection = database.GetCollection<ServiceOrder>("ServiceOrders");
    }

    // Create a new service order and insert it into MongoDB
    public async Task CreateServiceOrder(ServiceOrder order)
    {
        order.CreatedAt = DateTime.Now;  // Set the CreatedAt field to the current date and time
        order.UpdatedAt = DateTime.Now;  // Set the UpdatedAt field to the current date and time
        await _serviceOrdersCollection.InsertOneAsync(order);  // Insert the new order into the database
    }

    // Get a service order by ID and User ID, ensuring the user can only access their own order
    public async Task<ServiceOrder> GetServiceOrderById(string id, string userId)
    {
        return await _serviceOrdersCollection
            .Find(order => order.Id == id && order.UserId == userId)  // Ensure the order belongs to the specified user
            .FirstOrDefaultAsync();  // Return the first match or null if not found
    }

    // Get all service orders for a specific user based on their User ID
    public async Task<List<ServiceOrder>> GetServiceOrdersByUserId(string userId)
    {
        return await _serviceOrdersCollection
            .Find(order => order.UserId == userId)  // Find orders where the UserId matches
            .ToListAsync();  // Return the list of orders
    }

    // Get all service orders (admin view, returns all orders regardless of user)
    public async Task<List<ServiceOrder>> GetServiceOrders()
    {
        return await _serviceOrdersCollection
            .Find(order => true)  // Find all orders (no filter)
            .ToListAsync();  // Return the list of all orders
    }

    // Update an existing service order by its ID with the updated information
    public async Task UpdateServiceOrder(string id, ServiceOrder updatedOrder)
    {
        updatedOrder.UpdatedAt = DateTime.Now;  // Update the UpdatedAt field to the current date and time
        await _serviceOrdersCollection
            .ReplaceOneAsync(order => order.Id == id, updatedOrder);  // Replace the order with updated data
    }

    // Delete a service order by its ID
    public async Task DeleteServiceOrder(string id)
    {
        await _serviceOrdersCollection
            .DeleteOneAsync(order => order.Id == id);  // Delete the order with the specified ID
    }
}
