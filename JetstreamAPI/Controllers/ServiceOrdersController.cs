using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]  // Marks the class as a controller for API requests
[Route("api/[controller]")]  // Sets the base route for the controller
public class ServiceOrdersController : ControllerBase
{
    private readonly MongoDbService _mongoDbService;

    // Constructor that initializes MongoDbService for database interactions
    public ServiceOrdersController(MongoDbService mongoDbService)
    {
        _mongoDbService = mongoDbService;
    }

    // Endpoint to register a new user (for completeness, assuming UserService is implemented)
    // POST: api/serviceorders/register
    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(User user)
    {
        // Add logic to check if the user already exists (add UserService for this part)
        return Ok(user);  // Returns the created user
    }

    // Endpoint to log in a user and return a JWT token
    // POST: api/serviceorders/login
    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(User credentials)
    {
        // Add logic to authenticate the user
        return Ok("JWT Token");  // Return JWT token if valid
    }

    // Endpoint to place a new service order for the logged-in user
    // POST: api/serviceorders/order
    [HttpPost("order")]
    public async Task<ActionResult<ServiceOrder>> PlaceOrder(ServiceOrder order)
    {
        var userId = "currentLoggedInUserId";  // Extract user ID from JWT or session
        order.UserId = userId;  // Assign the user ID to the order
        await _mongoDbService.CreateServiceOrder(order);  // Save the order in the database
        return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);  // Return the created order with a link to get it by ID
    }

    // Endpoint to get all orders for the logged-in user
    // GET: api/serviceorders/orders
    [HttpGet("orders")]
    public async Task<ActionResult<List<ServiceOrder>>> GetOrders()
    {
        var userId = "currentLoggedInUserId";  // Extract user ID from JWT or session
        var orders = await _mongoDbService.GetServiceOrdersByUserId(userId);  // Fetch orders for the user
        return Ok(orders);  // Return the list of orders
    }

    // Endpoint to get a specific order by ID for the logged-in user
    // GET: api/serviceorders/orders/{id}
    [HttpGet("orders/{id}")]
    public async Task<ActionResult<ServiceOrder>> GetOrderById(string id)
    {
        var userId = "currentLoggedInUserId";  // Extract user ID from JWT or session
        var order = await _mongoDbService.GetServiceOrderById(id, userId);  // Fetch the order by ID for the user

        if (order == null)
        {
            return NotFound();  // Return 404 if the order is not found
        }
        return Ok(order);  // Return the order if found
    }

    // Endpoint to update the status of an order (only user or admin can update)
    // PATCH: api/serviceorders/{id}/status
    [HttpPatch("{id}/status")]
    public async Task<ActionResult> UpdateStatus(string id, [FromBody] string status)
    {
        var userId = "currentLoggedInUserId";  // Extract user ID from JWT or session
        var order = await _mongoDbService.GetServiceOrderById(id, userId);  // Fetch the order by ID for the user

        if (order == null || order.UserId != userId)
        {
            return Unauthorized("You are not authorized to update this order.");  // Return 401 if the user is not authorized
        }

        order.Status = status;  // Update the order status
        await _mongoDbService.UpdateServiceOrder(id, order);  // Save the updated order
        return NoContent();  // Return 204 for successful update with no content
    }

    // Endpoint to update a service order (only user or admin can update)
    // PUT: api/serviceorders/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateOrder(string id, ServiceOrder updatedOrder)
    {
        var userId = "currentLoggedInUserId";  // Extract user ID from JWT or session
        var order = await _mongoDbService.GetServiceOrderById(id, userId);  // Fetch the order by ID for the user

        if (order == null || order.UserId != userId)
        {
            return Unauthorized("You are not authorized to update this order.");  // Return 401 if the user is not authorized
        }

        await _mongoDbService.UpdateServiceOrder(id, updatedOrder);  // Save the updated order
        return NoContent();  // Return 204 for successful update with no content
    }

    // Endpoint to delete a service order (only user or admin can delete)
    // DELETE: api/serviceorders/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOrder(string id)
    {
        var userId = "currentLoggedInUserId";  // Extract user ID from JWT or session
        var order = await _mongoDbService.GetServiceOrderById(id, userId);  // Fetch the order by ID for the user

        if (order == null || order.UserId != userId)
        {
            return Unauthorized("You are not authorized to delete this order.");  // Return 401 if the user is not authorized
        }

        await _mongoDbService.DeleteServiceOrder(id);  // Delete the order from the database
        return NoContent();  // Return 204 for successful deletion with no content
    }
}
