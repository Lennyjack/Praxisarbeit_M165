using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

// Class representing a service order in the system
public class ServiceOrder
{
    // Unique identifier for the service order, stored as ObjectId in MongoDB
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    // User ID associated with the service order
    [BsonElement("userId")]
    public string UserId { get; set; }

    // Type of service for the order (e.g., "Kleiner Service", "Grosser Service")
    [BsonElement("serviceType")]
    public string ServiceType { get; set; }

    // Status of the order (e.g., "Offen", "InArbeit", "Abgeschlossen")
    [BsonElement("status")]
    public string Status { get; set; }

    // Priority of the order (e.g., "High", "Low")
    [BsonElement("priority")]
    public string Priority { get; set; }

    // Date and time when the service order was created
    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    // Date and time when the service order was last updated
    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; }
}
