using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

// Class representing a user in the system
public class User
{
    // Unique identifier for the user, stored as ObjectId in MongoDB
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    // Username of the user, used for login and identification
    [BsonElement("username")]
    public string Username { get; set; }

    // Hashed password for user authentication
    [BsonElement("passwordHash")]
    public string PasswordHash { get; set; }

    // Role of the user, e.g., "user" or "admin"
    [BsonElement("role")]
    public string Role { get; set; }
}
