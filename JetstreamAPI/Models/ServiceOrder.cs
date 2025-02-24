public class ServiceOrder
{
    public string Id { get; set; }
    public string CustomerName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Priority { get; set; }
    public string ServiceType { get; set; }  // For example, "Kleiner Service", "Grosser Service"
    public string Status { get; set; }  // "Offen", "InArbeit", "Abgeschlossen"
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
