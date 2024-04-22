namespace ToDoAPI.Models
{
    public class ToDo
    {
        public required Guid EventId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
