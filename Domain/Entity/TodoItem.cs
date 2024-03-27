namespace Domain.Entity;

public class TodoItem
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }

    // Soft delete capability
    public bool IsDelete { get; set; }
}
